using System.Threading.Tasks;

namespace MultiLangTranslator
{
    /// <summary>
    /// Çeviri servisleri için ortak arayüz.
    /// Hem offline (sözlük tabanlı) hem de online (DeepL gibi API'ler) bu arayüzü implement eder.
    /// Böylece form kodu hangi servisin kullanıldığını bilmek zorunda kalmaz.
    /// </summary>
    public interface ITranslationService
    {
        /// <summary>
        /// Uygulamada kullanıcıya gösterilecek desteklenen dilleri döndürür.
        /// </summary>
        string[] GetSupportedLanguages();

        /// <summary>
        /// Verilen metni, kaynak dilden hedef dile asenkron olarak çevirir.
        /// </summary>
        /// <param name="text">Çevrilecek metin.</param>
        /// <param name="sourceLanguage">Kaynak dil adı (ör: "Türkçe").</param>
        /// <param name="targetLanguage">Hedef dil adı (ör: "İngilizce").</param>
        /// <returns>Çevrilmiş metni içeren Task.</returns>
        Task<string> TranslateAsync(string text, string sourceLanguage, string targetLanguage);
    }
}


