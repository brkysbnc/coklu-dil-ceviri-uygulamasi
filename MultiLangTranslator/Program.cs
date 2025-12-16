using System;
using System.Windows.Forms;

namespace MultiLangTranslator
{
    /// <summary>
    /// Uygulamanın giriş noktası.
    /// Bu sınıf sadece WinForms altyapısını başlatır ve ana formu (`MainForm`) açar.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana giriş metodu.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // WinForms için gerekli klasik başlangıç konfigürasyonlarını yapar.
            // (ApplicationConfiguration.Initialize() yerine daha genel ve uyumlu yöntem)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ana formu başlatır.
            Application.Run(new MainForm());
        }
    }
}


