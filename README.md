## Çoklu Dil Çeviri Uygulaması (C# WinForms)

Bu proje, **C# ve Windows Forms** kullanılarak geliştirilmiş basit ve anlaşılır bir **çoklu dil çeviri uygulamasıdır**. Uygulama, internet bağlantısı gerektirmeden (offline) örnek bir sözlük üzerinden metinleri farklı diller arasında çevirebilir.

### Özellikler

- **Basit ve anlaşılır arayüz**
- **Kaynak dil** ve **hedef dil** seçimi (örnek: Türkçe, İngilizce, Almanca, Fransızca)
- **Girdi metni alanı** ve **çeviri sonucu alanı**
- **Çeviri yap** butonu ile hızlı sonuç
- **Dilleri Değiştir** butonu (kaynak–hedef dillerini tek tıkla yer değiştir)
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
  - `MyMemoryTranslationService.cs` – Online çeviri servisi (API key gerektirmez)  
  - `AzureTranslatorService.cs` – Azure Translator API entegrasyonu  
  - `ITranslationService.cs` – Çeviri servisleri için arayüz  
  - `AppSettings.cs` – Uygulama ayarları  
  - `Data/OfflineDictionary.json` – JSON formatında offline sözlük dosyası  

---

### Gereksinimler

- **İşletim Sistemi**: Windows 10 veya üzeri  
- **.NET SDK**: .NET 8 (veya yüklü olan uygun .NET Desktop Runtime)  
- Tercihen **Visual Studio 2022** (Community sürümü yeterlidir) veya `dotnet` CLI ile derleme

---

### Kurulum ve Çalıştırma

1. **Projeyi klonlayın veya indirin**

   ```bash
   git clone https://github.com/brkysbnc/coklu-dil-ceviri-uygulamasi.git
   cd coklu-dil-ceviri-uygulamasi
   ```

2. **Visual Studio ile açma (Önerilen)**

   - `MultiLangTranslator.sln` dosyasını Visual Studio ile açın.  
   - Gerekirse .NET Desktop Development workload'un yüklü olduğundan emin olun.  
   - Üst menüden **Build > Build Solution** ile projeyi derleyin.  
   - Ardından **Start** (F5) ile uygulamayı çalıştırın.

3. **.NET CLI ile çalıştırma (alternatif)**

   ```bash
   cd MultiLangTranslator
   dotnet run
   ```

4. **.exe Oluşturma**

   - Visual Studio'da **Build > Publish** seçeneğini kullanabilirsiniz.
   - Veya Release modunda derleyip `bin\Release\net8.0-windows\MultiLangTranslator.exe` dosyasını kullanabilirsiniz.

---

### Kullanım

- **Kaynak Dil** açılır kutusundan çevirmek istediğiniz metnin dilini seçin.  
- **Hedef Dil** açılır kutusundan çevrilecek dili seçin.  
- Üstteki metin kutusuna çevirmek istediğiniz cümleyi yazın.  
- **Çevir** butonuna tıklayın, alt metin kutusunda çeviri sonucu görüntülenir.  
- **Dilleri Değiştir** butonuna basarak kaynak ve hedef dilleri hızlıca yer değiştirebilirsiniz.

> **Not**: Uygulama varsayılan olarak **MyMemory Translation API** (ücretsiz, API key gerektirmez) kullanarak online çeviri yapar. İsterseniz `AppSettings.cs` dosyasında Azure Translator ayarlarını yaparak Azure servisini de kullanabilirsiniz. Ayrıca offline mod için `Data/OfflineDictionary.json` dosyasına kelime/cümle ekleyebilirsiniz.

---

### Çeviri Servisleri

Uygulama üç farklı çeviri yöntemi destekler:

1. **MyMemory Translation API** (Varsayılan)
   - API key gerektirmez
   - Ücretsiz kullanım
   - İnternet bağlantısı gerekir

2. **Azure Translator API** (Opsiyonel)
   - `AppSettings.cs` dosyasında API key, endpoint ve region bilgilerini doldurmanız gerekir
   - Daha yüksek kaliteli çeviriler
   - İnternet bağlantısı gerekir

3. **Offline Sözlük** (Fallback)
   - `Data/OfflineDictionary.json` dosyasından okunur
   - İnternet bağlantısı gerektirmez
   - Sınırlı kelime/cümle desteği

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
