using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;
using PST.Models;
using System;
using System.Drawing;
using PST.Modules;
using PST.Services;
using System.Threading.Tasks;

namespace PST.Views.Pages
{
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("=== HOMEPAGE YÜKLENDİ ===");
        }

        private void OnSelectRegionClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("🎯 EKRAN BÖLGESİ SEÇ butonuna tıklandı!");
        }

        private void OnCopyLastClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("📋 SON ÇEVİRİYİ KOPYALA butonuna tıklandı!");
        }

        private void OnRescanClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("🔄 AYNI BÖLGEYİ TEKRAR TARA butonuna tıklandı!");
        }

        private async void OnQuickTestClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("🚀 HIZLI TEST butonuna tıklandı - METHOD BAŞLADI!");

                // DURUMU GÜNCELLE
                StatusText.Text = "Test çalışıyor...";
                StatusIndicator.Foreground = Avalonia.Media.Brushes.Yellow;

                System.Diagnostics.Debug.WriteLine("1. Ekran yakalama modülü test ediliyor...");
                var region = new Rectangle(100, 100, 400, 200);
                var screenshot = await ModuleManager.ScreenCapture.CaptureRegionAsync(region);
                System.Diagnostics.Debug.WriteLine("✓ Ekran yakalama başarılı!");

                System.Diagnostics.Debug.WriteLine("2. Çeviri modülü test ediliyor...");
                var testText = "Hello world! This is a test text for translation.";
                var translatedText = await ModuleManager.Translation.TranslateAsync(testText, "tr");
                System.Diagnostics.Debug.WriteLine($"✓ Çeviri başarılı: {translatedText}");

                System.Diagnostics.Debug.WriteLine("3. Overlay modülü test ediliyor...");
                var settings = DisplaySettings.Load();
                ModuleManager.Overlay.ShowText(translatedText, new Avalonia.Point(100, 100), settings);
                System.Diagnostics.Debug.WriteLine("✓ Overlay gösterimi başarılı!");

                // BAŞARILI MESAJI GÖSTER
                StatusText.Text = "Tüm testler başarılı!";
                StatusIndicator.Foreground = Avalonia.Media.Brushes.Green;

                System.Diagnostics.Debug.WriteLine("🚀 TÜM TESTLER TAMAMLANDI!");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ TEST HATASI: {ex}");
                StatusText.Text = "Test hatası! Detaylar için Output'a bakın.";
                StatusIndicator.Foreground = Avalonia.Media.Brushes.Red;

                // Kullanıcıya hata mesajı göster
                ErrorHandler.ShowUserFriendlyMessage($"Test sırasında hata: {ex.Message}");
            }
        }
    }
}