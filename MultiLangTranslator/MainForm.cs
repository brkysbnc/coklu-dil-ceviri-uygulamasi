using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiLangTranslator
{
    public partial class MainForm : Form
    {
        // Uygulamanın çeviri mantığını yöneten servis arayüzü.
        // Bu sayede ister offline sözlük, ister DeepL gibi online servisler kullanılabilir.
        private readonly ITranslationService _translationService;

        public MainForm()
        {
            InitializeComponent();
            // Öncelik sırasına göre uygun çeviri servisini seçiyoruz:
            // 1) Azure Translator ayarları doluysa Azure üzerinden online çeviri kullan.
            // 2) Aksi halde MyMemory (API key gerektirmeyen) online servisini dene.
            // 3) O da başarısız olursa offline sözlük tabanlı çeviri kullan.
            if (!string.IsNullOrWhiteSpace(AppSettings.AzureTranslatorKey) &&
                !string.IsNullOrWhiteSpace(AppSettings.AzureTranslatorEndpoint) &&
                !string.IsNullOrWhiteSpace(AppSettings.AzureTranslatorRegion))
            {
                _translationService = new AzureTranslatorService(
                    AppSettings.AzureTranslatorEndpoint,
                    AppSettings.AzureTranslatorRegion,
                    AppSettings.AzureTranslatorKey);
            }
            else
            {
                // MyMemory için özel ayara gerek yok; basit bir HTTP GET ile çalışıyor.
                // Herhangi bir sebeple hata dönerse, kullanıcı hata mesajını görecek,
                // ama uygulama yine de çalışmaya devam edecek.
                _translationService = new MyMemoryTranslationService();
            }
        }

        /// <summary>
        /// Form yüklendiğinde çalışır. Dil listelerini hazırlar ve varsayılan seçimleri ayarlar.
        /// </summary>
        private void MainForm_Load(object? sender, EventArgs e)
        {
            // Desteklenen dilleri servisten alıp combobox'lara ekliyoruz.
            var languages = _translationService.GetSupportedLanguages();
            cmbSourceLanguage.Items.AddRange(languages);
            cmbTargetLanguage.Items.AddRange(languages);

            // Varsayılan olarak Türkçe -> İngilizce çeviri seçelim.
            if (cmbSourceLanguage.Items.Count > 0)
            {
                var trIndex = cmbSourceLanguage.Items.IndexOf("Türkçe");
                cmbSourceLanguage.SelectedIndex = trIndex >= 0 ? trIndex : 0;
            }

            if (cmbTargetLanguage.Items.Count > 0)
            {
                var enIndex = cmbTargetLanguage.Items.IndexOf("İngilizce");
                cmbTargetLanguage.SelectedIndex = enIndex >= 0 ? enIndex : 0;
            }
        }

        /// <summary>
        /// "Çevir" butonuna basıldığında tetiklenir.
        /// Kullanıcının girdiği metni seçilen dillere göre çevirir.
        /// </summary>
        private async void BtnTranslate_Click(object? sender, EventArgs e)
        {
            var sourceLanguage = cmbSourceLanguage.SelectedItem?.ToString();
            var targetLanguage = cmbTargetLanguage.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(sourceLanguage) || string.IsNullOrWhiteSpace(targetLanguage))
            {
                MessageBox.Show(
                    "Lütfen kaynak ve hedef dili seçiniz.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var inputText = txtSourceText.Text.Trim();
            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show(
                    "Lütfen çevirmek istediğiniz metni giriniz.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Çeviri servisini kullanarak metni asenkron şekilde çeviriyoruz.
            var result = await _translationService.TranslateAsync(inputText, sourceLanguage, targetLanguage);
            txtTranslatedText.Text = result;
        }

        /// <summary>
        /// "Dilleri Değiştir" butonuna basıldığında kaynak ve hedef dillerin yerini değiştirir.
        /// Mevcut metinleri de takas eder.
        
        private void BtnSwapLanguages_Click(object? sender, EventArgs e)
        {
            if (cmbSourceLanguage.SelectedItem == null || cmbTargetLanguage.SelectedItem == null)
            {
                return;
            }

            // Seçili dilleri takas et
            var temp = cmbSourceLanguage.SelectedItem;
            cmbSourceLanguage.SelectedItem = cmbTargetLanguage.SelectedItem;
            cmbTargetLanguage.SelectedItem = temp;

            // Metin kutularını da takas edelim (kullanıcı için pratik)
            var sourceText = txtSourceText.Text;
            txtSourceText.Text = txtTranslatedText.Text;
            txtTranslatedText.Text = sourceText;
        }
    }
}


