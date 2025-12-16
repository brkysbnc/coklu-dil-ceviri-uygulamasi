using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiLangTranslator
{
    /// <summary>
    /// MyMemory (translated.net) servislerini kullanan basit online çeviri servisi.
    /// Avantajı: Temel kullanım için API anahtarı gerektirmez, hızlıca çalıştırılabilir.
    /// Dezavantajı: Ücretsiz katmanda hız ve doğruluk garantisi sınırlıdır, eğitim amaçlı idealdir.
    /// </summary>
    public class MyMemoryTranslationService : ITranslationService
    {
        private const string ApiUrl = "https://api.mymemory.translated.net/get";

        private readonly HttpClient _httpClient;

        // Kullanıcıya gösterilen dilden MyMemory dil koduna map.
        private readonly Dictionary<string, string> _languageCodeMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Türkçe", "tr" },
            { "İngilizce", "en" },
            { "Almanca", "de" },
            { "Fransızca", "fr" }
        };

        public MyMemoryTranslationService(HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }

        public string[] GetSupportedLanguages()
        {
            // UI ile uyumlu dört dili destekliyoruz.
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
                return "(Seçilen diller için MyMemory dil kodu tanımlı değil.)";
            }

            // Örnek istek: https://api.mymemory.translated.net/get?q=Hello%20world!&langpair=en|it
            var uri = $"{ApiUrl}?q={Uri.EscapeDataString(text)}&langpair={fromCode}|{toCode}";

            try
            {
                var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return $"(MyMemory hata: {response.StatusCode} - {json})";
                }

                // Örnek yanıt:
                // {
                //   "responseData": {
                //     "translatedText": "Ciao mondo",
                //     ...
                //   },
                //   ...
                // }
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                if (root.TryGetProperty("responseData", out var responseData) &&
                    responseData.TryGetProperty("translatedText", out var translatedTextElement))
                {
                    return translatedTextElement.GetString() ?? string.Empty;
                }

                return "(MyMemory yanıtı beklenen formatta değil.)";
            }
            catch (Exception ex)
            {
                // Ağ hatasında kullanıcıya bilgilendirici mesaj döndür.
                return $"(MyMemory isteği sırasında bir hata oluştu: {ex.Message})";
            }
        }
    }
}


