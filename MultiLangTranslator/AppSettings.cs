using System;

namespace MultiLangTranslator
{
    /// <summary>
    /// Uygulama genelinde kullanılan basit ayar sınıfı.
    /// Burada özellikle online çeviri servislerine ait API anahtarlarını tutuyoruz.
    /// Gerçek projede bu değerler genelde config dosyasından veya gizli ayarlardan okunur.
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// DeepL API Free planından aldığınız API anahtarını buraya yazın.
        /// Örn: "12345678-xxxx-xxxx-xxxx-xxxxxxxxxxxx:fx"
        /// Boş bırakırsanız DeepL kullanılmaz.
        /// (Şu an varsayılan olarak boş bırakıyoruz; istersen sonradan kullanabilirsin.)
        /// </summary>
        public static string DeepLApiKey { get; set; } = "";

        /// <summary>
        /// Microsoft Translator (Azure AI Translator) servisi için API anahtarı.
        /// Azure Portal'da oluşturduğun Translator kaynağının "Anahtarlar ve Uç Nokta" kısmından alabilirsin.
        /// </summary>
        public static string AzureTranslatorKey { get; set; } = "";

        /// <summary>
        /// Microsoft Translator servisi için uç nokta (endpoint) adresi.
        /// Örn: "https://api.cognitive.microsofttranslator.com"
        /// Azure Portal'daki Translator kaynağında yazar.
        /// </summary>
        public static string AzureTranslatorEndpoint { get; set; } = "";

        /// <summary>
        /// Microsoft Translator servisi için bölge (region) bilgisi.
        /// Örn: "westeurope", "northeurope" vb.
        /// Bu değer yine Azure Portal'daki Translator kaynağında yer alır.
        /// </summary>
        public static string AzureTranslatorRegion { get; set; } = "";
    }
}


