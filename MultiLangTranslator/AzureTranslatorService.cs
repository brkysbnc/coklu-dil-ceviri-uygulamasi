using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiLangTranslator
{
    /// <summary>
    /// Microsoft Translator (Azure AI Translator) API'si üzerinden online çeviri yapan servis.
    /// ITranslationService arayüzünü implement eder ve HTTP isteği ile çeviri yapar.
    /// </summary>
    public class AzureTranslatorService : ITranslationService
    {
        private readonly string _endpoint;
        private readonly string _region;
        private readonly string _key;
        private readonly HttpClient _httpClient;

        // Kullanıcıya gösterilen dil adından Azure Translator dil koduna map.
        private readonly Dictionary<string, string> _languageCodeMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Türkçe", "tr" },
            { "İngilizce", "en" },
            { "Almanca", "de" },
            { "Fransızca", "fr" }
        };

        public AzureTranslatorService(string endpoint, string region, string key, HttpClient? httpClient = null)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentException("Azure Translator endpoint değeri boş olamaz.", nameof(endpoint));
            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentException("Azure Translator region değeri boş olamaz.", nameof(region));
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Azure Translator key değeri boş olamaz.", nameof(key));

            _endpoint = endpoint.TrimEnd('/');
            _region = region;
            _key = key;
            _httpClient = httpClient ?? new HttpClient();
        }

        public string[] GetSupportedLanguages()
        {
            // Azure Translator çok daha fazla dili destekler,
            // ama biz UI ile uyumlu olması için yine bu dört dili gösteriyoruz.
            return new[]
            {
                "Türkçe",
                "İngilizce",
                "Almanca",
                "Fransızca"
            };
        }

        public async Task<string> TranslateAsync(string text, string sourceLanguage, string targetLanguage)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            if (string.Equals(sourceLanguage, targetLanguage, StringComparison.OrdinalIgnoreCase))
                return text;

            if (!_languageCodeMap.TryGetValue(sourceLanguage, out var fromCode) ||
                !_languageCodeMap.TryGetValue(targetLanguage, out var toCode))
            {
                return "(Seçilen diller için Azure Translator dil kodu tanımlı değil.)";
            }

            // Azure Translator çeviri endpoint'i (v3)
            var route = $"/translate?api-version=3.0&from={fromCode}&to={toCode}";
            var requestUri = _endpoint + route;

            // Gövde: çevirilecek metinleri içeren JSON dizisi
            var body = new[]
            {
                new { Text = text }
            };
            var requestBody = JsonSerializer.Serialize(body);
            using var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Gerekli header'lar (Ocp-Apim-Subscription-Key ve Region)
            using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Content = content;
            request.Headers.Add("Ocp-Apim-Subscription-Key", _key);
            request.Headers.Add("Ocp-Apim-Subscription-Region", _region);

            try
            {
                var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return $"(Azure Translator hata: {response.StatusCode} - {responseBody})";
                }

                // Örnek yanıt:
                // [
                //   {
                //     "translations": [
                //       { "text": "Hallo Welt", "to": "de" }
                //     ]
                //   }
                // ]
                var doc = JsonDocument.Parse(responseBody);
                var root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                {
                    var first = root[0];
                    if (first.TryGetProperty("translations", out var translations) &&
                        translations.ValueKind == JsonValueKind.Array &&
                        translations.GetArrayLength() > 0)
                    {
                        var firstTranslation = translations[0];
                        if (firstTranslation.TryGetProperty("text", out var textElement))
                        {
                            return textElement.GetString() ?? string.Empty;
                        }
                    }
                }

                return "(Azure Translator yanıtı beklenen formatta değil.)";
            }
            catch (Exception ex)
            {
                return $"(Azure Translator isteği sırasında bir hata oluştu: {ex.Message})";
            }
        }
    }
}


