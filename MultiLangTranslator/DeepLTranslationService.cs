using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiLangTranslator
{
    /// <summary>
    /// DeepL API Free üzerinden online çeviri yapan servis.
    /// Bu sınıf, ITranslationService arayüzünü implement eder ve HTTP isteği ile gerçek çeviri yapar.
    /// </summary>
    public class DeepLTranslationService : ITranslationService
    {
        private const string ApiUrl = "https://api-free.deepl.com/v2/translate";

        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        // Görünen dil adından (Türkçe, İngilizce...) DeepL dil koduna (TR, EN...) map.
        private readonly Dictionary<string, string> _languageCodeMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Türkçe", "TR" },
            { "İngilizce", "EN" },
            { "Almanca", "DE" },
            { "Fransızca", "FR" }
        };

        /// <summary>
        /// DeepL API anahtarını alarak servis örneği oluşturur.
        /// </summary>
        public DeepLTranslationService(string apiKey, HttpClient? httpClient = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("DeepL API anahtarı boş olamaz.", nameof(apiKey));
            }

            _apiKey = apiKey;
            _httpClient = httpClient ?? new HttpClient();
        }

        /// <inheritdoc />
        public string[] GetSupportedLanguages()
        {
            // DeepL bu dillerin hepsini desteklediği için, kullanıcıya offline servis ile aynı listeyi gösterebiliriz.
            return new[]
            {
                "Türkçe",
                "İngilizce",
                "Almanca",
                "Fransızca"
            };
        }

        /// <inheritdoc />
        public async Task<string> TranslateAsync(string text, string sourceLanguage, string targetLanguage)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            if (string.Equals(sourceLanguage, targetLanguage, StringComparison.OrdinalIgnoreCase))
            {
                return text;
            }

            if (!_languageCodeMap.TryGetValue(sourceLanguage, out var sourceCode) ||
                !_languageCodeMap.TryGetValue(targetLanguage, out var targetCode))
            {
                return "(Seçilen diller için DeepL dil kodu tanımlı değil.)";
            }

            // DeepL API'ye gönderilecek form verisini hazırlıyoruz.
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("auth_key", _apiKey),
                new KeyValuePair<string, string>("text", text),
                new KeyValuePair<string, string>("source_lang", sourceCode),
                new KeyValuePair<string, string>("target_lang", targetCode)
            });

            try
            {
                var response = await _httpClient.PostAsync(ApiUrl, requestContent).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return $"(DeepL hata: {response.StatusCode} - {errorBody})";
                }

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                // DeepL yanıt yapısı:
                // { "translations": [ { "detected_source_language": "EN", "text": "Merhaba dünya" } ] }
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                if (root.TryGetProperty("translations", out var translationsElement) &&
                    translationsElement.GetArrayLength() > 0)
                {
                    var first = translationsElement[0];
                    if (first.TryGetProperty("text", out var textElement))
                    {
                        return textElement.GetString() ?? string.Empty;
                    }
                }

                return "(DeepL yanıtı beklenen formatta değil.)";
            }
            catch (Exception ex)
            {
                // Ağ hatası vb. durumlarda kullanıcıya açıklayıcı bir mesaj döndürüyoruz.
                return $"(DeepL isteği sırasında bir hata oluştu: {ex.Message})";
            }
        }
    }
}


