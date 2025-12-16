using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiLangTranslator
{
    /// <summary>
    /// Basit, örnek amaçlı offline çeviri servisi.
    /// Gerçek bir projede burası API entegrasyonu veya kapsamlı bir sözlük ile değiştirilebilir.
    /// Burada sadece birkaç dil ve kelime/cümle için örnek çeviriler bulunmaktadır.
    /// </summary>
    public class TranslationService : ITranslationService
    {
        // Anahtar: "kaynakDil|hedefDil|metinKüçükHarf"
        // Değer: çevrilmiş metin
        private readonly Dictionary<string, string> _dictionary;

        public TranslationService()
        {
            // Önce JSON dosyasından sözlüğü yüklemeyi deneriz.
            // Eğer dosya yoksa veya okunamazsa, örnek sabit sözlüğe geri düşeriz.
            _dictionary = LoadDictionaryFromJsonOrFallback();
        }

        /// <inheritdoc />
        public string[] GetSupportedLanguages()
        {
            return new[]
            {
                "Türkçe",
                "İngilizce",
                "Almanca",
                "Fransızca"
            };
        }

        /// <inheritdoc />
        public Task<string> TranslateAsync(string text, string sourceLanguage, string targetLanguage)
        {
            // Offline çeviri için senkron mantığı koruyup Task'a sarıyoruz.
            var result = TranslateInternal(text, sourceLanguage, targetLanguage);
            return Task.FromResult(result);
        }

        /// <summary>
        /// Verilen metni, kaynak dilden hedef dile çeviren iç senkron metod.
        /// Sözlükte birebir karşılığı yoksa, bilgilendirici bir mesaj döndürür.
        /// </summary>
        private string TranslateInternal(string text, string sourceLanguage, string targetLanguage)
        {
            if (string.Equals(sourceLanguage, targetLanguage, StringComparison.OrdinalIgnoreCase))
            {
                // Aynı dil seçilmişse, metni olduğu gibi geri döndürüyoruz.
                return text;
            }

            // Anahtar formatını oluştur (diller ve metin küçük harfe çevrilir).
            var key = BuildKey(sourceLanguage, targetLanguage, text);

            if (_dictionary.TryGetValue(key, out var translated))
            {
                return translated;
            }

            // Karşılığı yoksa kullanıcıya basit bir mesaj göster.
            return $"(Örnek sözlükte bu ifade bulunamadı: \"{text}\")";
        }

        /// <summary>
        /// Sözlük anahtarı oluşturmak için yardımcı metod.
        /// </summary>
        private static string BuildKey(string sourceLanguage, string targetLanguage, string text)
        {
            // Küçük harfe çevirirken kültür bağımlılığını önlemek için InvariantCulture kullanıyoruz.
            var src = sourceLanguage.Trim().ToLower(CultureInfo.InvariantCulture);
            var dst = targetLanguage.Trim().ToLower(CultureInfo.InvariantCulture);
            var normalizedText = text.Trim().ToLower(CultureInfo.InvariantCulture);
            return $"{src}|{dst}|{normalizedText}";
        }

        /// <summary>
        /// JSON dosyasından sözlüğü yüklemeyi dener. Başarısız olursa sabit, gömülü örnek sözlüğe geri döner.
        /// Bu sayede kullanıcı sadece JSON dosyasını düzenleyerek yeni kelime/cümleler ekleyebilir.
        /// </summary>
        private static Dictionary<string, string> LoadDictionaryFromJsonOrFallback()
        {
            try
            {
                var dictFromFile = LoadFromJson();
                if (dictFromFile.Count > 0)
                {
                    return dictFromFile;
                }
            }
            catch
            {
                // Dosya yoksa veya bozuksa hata fırlatmak yerine gömülü sözlüğe düşüyoruz.
            }

            return BuildEmbeddedSampleDictionary();
        }

        /// <summary>
        /// Uygulama çıktısındaki Data/OfflineDictionary.json dosyasını okuyarak sözlük oluşturur.
        /// </summary>
        private static Dictionary<string, string> LoadFromJson()
        {
            var result = new Dictionary<string, string>();

            // Çalışan .exe'nin bulunduğu dizini baz alarak Data klasörünü buluyoruz.
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var jsonPath = Path.Combine(baseDir, "Data", "OfflineDictionary.json");

            if (!File.Exists(jsonPath))
            {
                return result;
            }

            var json = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var items = JsonSerializer.Deserialize<List<DictionaryItem>>(json, options) ?? new List<DictionaryItem>();

            foreach (var item in items)
            {
                if (string.IsNullOrWhiteSpace(item.SourceLanguage) ||
                    string.IsNullOrWhiteSpace(item.TargetLanguage) ||
                    string.IsNullOrWhiteSpace(item.SourceText) ||
                    string.IsNullOrWhiteSpace(item.TargetText))
                {
                    continue;
                }

                result[BuildKey(item.SourceLanguage, item.TargetLanguage, item.SourceText)] = item.TargetText;
            }

            return result;
        }

        /// <summary>
        /// Örnek amaçlı sabit bir sözlük oluşturur (fallback).
        /// JSON dosyası okunamazsa bu veriler kullanılır.
        /// </summary>
        private static Dictionary<string, string> BuildEmbeddedSampleDictionary()
        {
            var dict = new Dictionary<string, string>();

            // Yardımcı lokal fonksiyon: sözlüğe çift yönlü çeviri ekler.
            void AddPair(string langFrom, string langTo, string from, string to)
            {
                dict[BuildKey(langFrom, langTo, from)] = to;
                dict[BuildKey(langTo, langFrom, to)] = from;
            }

            // Türkçe <-> İngilizce
            AddPair("Türkçe", "İngilizce", "merhaba", "hello");
            AddPair("Türkçe", "İngilizce", "günaydın", "good morning");
            AddPair("Türkçe", "İngilizce", "teşekkür ederim", "thank you");
            AddPair("Türkçe", "İngilizce", "nasılsın", "how are you");

            // Türkçe <-> Almanca
            AddPair("Türkçe", "Almanca", "teşekkür ederim", "danke");
            AddPair("Türkçe", "Almanca", "günaydın", "guten morgen");
            AddPair("Türkçe", "Almanca", "iyi akşamlar", "guten abend");

            // Türkçe <-> Fransızca
            AddPair("Türkçe", "Fransızca", "merhaba", "bonjour");
            AddPair("Türkçe", "Fransızca", "teşekkür ederim", "merci");
            AddPair("Türkçe", "Fransızca", "günaydın", "bonjour");

            // İngilizce <-> Almanca
            AddPair("İngilizce", "Almanca", "hello", "hallo");
            AddPair("İngilizce", "Almanca", "good morning", "guten morgen");

            // İngilizce <-> Fransızca
            AddPair("İngilizce", "Fransızca", "hello", "bonjour");
            AddPair("İngilizce", "Fransızca", "good night", "bonne nuit");

            return dict;
        }

        /// <summary>
        /// JSON sözlük satırlarını modellemek için kullanılan basit DTO sınıfı.
        /// </summary>
        private sealed class DictionaryItem
        {
            public string SourceLanguage { get; set; } = string.Empty;
            public string TargetLanguage { get; set; } = string.Empty;
            public string SourceText { get; set; } = string.Empty;
            public string TargetText { get; set; } = string.Empty;
        }
    }
}



