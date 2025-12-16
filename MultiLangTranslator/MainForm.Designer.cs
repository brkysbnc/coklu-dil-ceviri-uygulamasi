using System.Windows.Forms;

namespace MultiLangTranslator
{
    partial class MainForm
    {
        /// <summary>
        /// Tasarımcı tarafından kullanılan bileşen konteyneri.
        /// Form kapatılırken bu bileşenler dispose edilir.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label lblSourceLanguage;
        private Label lblTargetLanguage;
        private ComboBox cmbSourceLanguage;
        private ComboBox cmbTargetLanguage;
        private TextBox txtSourceText;
        private TextBox txtTranslatedText;
        private Button btnTranslate;
        private Button btnSwapLanguages;
        private Label lblInput;
        private Label lblOutput;

        /// <summary>
        /// Kullanılan tüm kaynakları temizler.
        /// </summary>
        /// <param name="disposing">Yönetilen kaynaklar dispose edilsin mi?</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Formun görsel bileşenlerini oluşturan metod.
        /// Bu metod, yalnızca tasarım amaçlıdır ve elle çok fazla değiştirilmemelidir.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblSourceLanguage = new Label();
            lblTargetLanguage = new Label();
            cmbSourceLanguage = new ComboBox();
            cmbTargetLanguage = new ComboBox();
            txtSourceText = new TextBox();
            txtTranslatedText = new TextBox();
            btnTranslate = new Button();
            btnSwapLanguages = new Button();
            lblInput = new Label();
            lblOutput = new Label();
            SuspendLayout();
            // 
            // lblSourceLanguage
            // 
            lblSourceLanguage.AutoSize = true;
            lblSourceLanguage.Location = new System.Drawing.Point(20, 20);
            lblSourceLanguage.Name = "lblSourceLanguage";
            lblSourceLanguage.Size = new System.Drawing.Size(71, 15);
            lblSourceLanguage.TabIndex = 0;
            lblSourceLanguage.Text = "Kaynak Dil:";
            // 
            // lblTargetLanguage
            // 
            lblTargetLanguage.AutoSize = true;
            lblTargetLanguage.Location = new System.Drawing.Point(260, 20);
            lblTargetLanguage.Name = "lblTargetLanguage";
            lblTargetLanguage.Size = new System.Drawing.Size(63, 15);
            lblTargetLanguage.TabIndex = 1;
            lblTargetLanguage.Text = "Hedef Dil:";
            // 
            // cmbSourceLanguage
            // 
            cmbSourceLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSourceLanguage.FormattingEnabled = true;
            cmbSourceLanguage.Location = new System.Drawing.Point(20, 40);
            cmbSourceLanguage.Name = "cmbSourceLanguage";
            cmbSourceLanguage.Size = new System.Drawing.Size(220, 23);
            cmbSourceLanguage.TabIndex = 2;
            // 
            // cmbTargetLanguage
            // 
            cmbTargetLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTargetLanguage.FormattingEnabled = true;
            cmbTargetLanguage.Location = new System.Drawing.Point(270, 40);
            cmbTargetLanguage.Name = "cmbTargetLanguage";
            cmbTargetLanguage.Size = new System.Drawing.Size(220, 23);
            cmbTargetLanguage.TabIndex = 3;
            // 
            // txtSourceText
            // 
            txtSourceText.Location = new System.Drawing.Point(20, 100);
            txtSourceText.Multiline = true;
            txtSourceText.Name = "txtSourceText";
            txtSourceText.ScrollBars = ScrollBars.Vertical;
            txtSourceText.Size = new System.Drawing.Size(440, 120);
            txtSourceText.TabIndex = 4;
            // 
            // txtTranslatedText
            // 
            txtTranslatedText.Location = new System.Drawing.Point(20, 250);
            txtTranslatedText.Multiline = true;
            txtTranslatedText.Name = "txtTranslatedText";
            txtTranslatedText.ReadOnly = true;
            txtTranslatedText.ScrollBars = ScrollBars.Vertical;
            txtTranslatedText.Size = new System.Drawing.Size(440, 120);
            txtTranslatedText.TabIndex = 5;
            // 
            // btnTranslate
            // 
            // "Dilleri Değiştir" butonunun hemen altına, sağ tarafa alıyoruz.
            btnTranslate.Location = new System.Drawing.Point(520, 75);
            btnTranslate.Name = "btnTranslate";
            btnTranslate.Size = new System.Drawing.Size(100, 25);
            btnTranslate.TabIndex = 6;
            btnTranslate.Text = "Çevir";
            btnTranslate.UseVisualStyleBackColor = true;
            btnTranslate.Click += BtnTranslate_Click;
            // 
            // btnSwapLanguages
            // 
            // Sağ tarafta, hedef dil combobox'ının yanında net şekilde görünecek konuma yerleştiriyoruz.
            btnSwapLanguages.Location = new System.Drawing.Point(520, 38);
            btnSwapLanguages.Name = "btnSwapLanguages";
            btnSwapLanguages.Size = new System.Drawing.Size(120, 27);
            btnSwapLanguages.TabIndex = 7;
            btnSwapLanguages.Text = "Dilleri Değiştir";
            btnSwapLanguages.UseVisualStyleBackColor = true;
            btnSwapLanguages.Click += BtnSwapLanguages_Click;
            // 
            // lblInput
            // 
            lblInput.AutoSize = true;
            lblInput.Location = new System.Drawing.Point(20, 80);
            lblInput.Name = "lblInput";
            lblInput.Size = new System.Drawing.Size(80, 15);
            lblInput.TabIndex = 8;
            lblInput.Text = "Girdi Metni:";
            // 
            // lblOutput
            // 
            lblOutput.AutoSize = true;
            // Sonuç metin kutusunun üstüne, sol tarafa alıyoruz.
            lblOutput.Location = new System.Drawing.Point(20, 230);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new System.Drawing.Size(88, 15);
            lblOutput.TabIndex = 9;
            lblOutput.Text = "Çeviri Sonucu:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(680, 400);
            Controls.Add(lblOutput);
            Controls.Add(lblInput);
            Controls.Add(btnSwapLanguages);
            Controls.Add(btnTranslate);
            Controls.Add(txtTranslatedText);
            Controls.Add(txtSourceText);
            Controls.Add(cmbTargetLanguage);
            Controls.Add(cmbSourceLanguage);
            Controls.Add(lblTargetLanguage);
            Controls.Add(lblSourceLanguage);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Çoklu Dil Çeviri Uygulaması";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}


