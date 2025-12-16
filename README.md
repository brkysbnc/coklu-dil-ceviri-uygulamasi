## Çoklu Dil Çeviri Uygulaması (C# WinForms)

Bu proje, **C# ve Windows Forms** kullanılarak geliştirilmiş basit ve anlaşılır bir **çoklu dil çeviri uygulamasıdır**. Uygulama, internet bağlantısı gerektirmeden (offline) örnek bir sözlük üzerinden metinleri farklı diller arasında çevirebilir.

### Özellikler

- **Basit ve anlaşılır arayüz**
- **Kaynak dil** ve **hedef dil** seçimi (örnek: Türkçe, İngilizce, Almanca, Fransızca)
- **Girdi metni alanı** ve **çeviri sonucu alanı**
- **Çeviri yap** butonu ile hızlı sonuç
- **Dilleri Değiştir** butonu (kaynak–hedef dillerini tek tıkla yer değiştir)
- Yapılan çevirilerin **geçmiş listesi** (son yapılan çevirileri görme)
- Tamamen **C# Windows Forms** ile yazılmıştır, internet bağlantısı gerektirmez (yerleşik örnek sözlük kullanır)

---

### Proje Yapısı

- `MultiLangTranslator.sln`  
  Visual Studio çözüm dosyası.

- `MultiLangTranslator/`  
  - `MultiLangTranslator.csproj` – Proje yapılandırma dosyası (.NET 8 WinForms)  
  - `Program.cs` – Uygulamanın giriş noktası  
  - `MainForm.cs` – Ana formun C# kodu  
  - `MainForm.Designer.cs` – Formun tasarım kodu (otomatik oluşturulur, elle değiştirmemeye çalışın)  
  - `TranslationService.cs` – Örnek offline çeviri sözlüğünü ve çeviri mantığını içeren sınıf  
  - `Properties/` – WinForms için otomatik oluşturulan ayarlar ve kaynak dosyaları  

---

### Gereksinimler

- **İşletim Sistemi**: Windows 10 veya üzeri  
- **.NET SDK**: .NET 8 (veya yüklü olan uygun .NET Desktop Runtime)  
- Tercihen **Visual Studio 2022** (Community sürümü yeterlidir) veya `dotnet` CLI ile derleme

---

### Kurulum ve Çalıştırma

1. **Projeyi klonlayın veya indirin**

   ```bash
   git clone <bu-repo-url>
   cd "Çoklu dil çevirisi uygulaması"
   ```

2. **Visual Studio ile açma (Önerilen)**

   - `MultiLangTranslator.sln` dosyasını Visual Studio ile açın.  
   - Gerekirse .NET Desktop Development workload’un yüklü olduğundan emin olun.  
   - Üst menüden **Build > Build Solution** ile projeyi derleyin.  
   - Ardından **Start** (F5) ile uygulamayı çalıştırın.

3. **.NET CLI ile çalıştırma (alternatif)**

   ```bash
   cd "Çoklu dil çevirisi uygulaması\\MultiLangTranslator"
   dotnet run
   ```

---

### Kullanım

- **Kaynak Dil** açılır kutusundan çevirmek istediğiniz metnin dilini seçin.  
- **Hedef Dil** açılır kutusundan çevrilecek dili seçin.  
- Üstteki metin kutusuna çevirmek istediğiniz cümleyi yazın.  
- **Çevir** butonuna tıklayın, alt metin kutusunda çeviri sonucu görüntülenir.  
- Yaptığınız her çeviri, sağdaki **Geçmiş** listesinin en üstüne eklenir.  
- **Dilleri Değiştir** butonuna basarak kaynak ve hedef dilleri hızlıca yer değiştirebilirsiniz.

> Not: Örnek olması için, uygulama içinde sınırlı sayıda hazır kelime/cümle içeren bir sözlük kullanılmıştır. Gerçek hayatta bu kısmı bir API (Google Translate, DeepL vb.) veya daha geniş bir veri seti ile değiştirebilirsiniz.

---

### Basit Görev Listesi (To-Do)

- **Daha Fazla Dil Ekle**: `TranslationService` içine yeni diller ve kelimeler ekleyin.  
- **Dosyadan Sözlük Yükleme**: JSON/CSV dosyasından dinamik sözlük okuma ekleyin.  
- **Metin Kopyala**: Çeviri sonucunu tek tıkla panoya kopyalama butonu ekleyin.  
- **Tema Desteği**: Açık/Koyu tema seçeneği ekleyin.  

---

### Notlar

- Bu proje, C# ve Windows Forms kullanarak **çoklu dil çevirisi** mantığını ve temel masaüstü arayüz geliştirme becerilerini göstermek amacıyla hazırlanmıştır.  
- Eğitim veya portföy amaçlı kullanıma uygundur.  
- İsterseniz projeyi fork edip kendi ihtiyaçlarınıza göre geliştirebilirsiniz.

---

### Öğrenci Bilgileri

- **Ad Soyad**: Berkay Sabuncu  
- **Fakülte**: Teknoloji Fakültesi  
- **Bölüm**: Yazılım Mühendisliği  
- **Sınıf / Şube**: II / A  
- **Öğrenci Numarası**: 240542029  



