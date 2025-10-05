using Avalonia;
using PST.Models;

namespace PST.Modules.Overlay
{
    public class OverlayService
    {
        public void ShowText(string text, Point position, DisplaySettings settings)
        {
            System.Diagnostics.Debug.WriteLine($"=== OVERLAY GÖSTERİMİ ===");
            System.Diagnostics.Debug.WriteLine($"Metin: {text}");
            System.Diagnostics.Debug.WriteLine($"Pozisyon: {position}");
            System.Diagnostics.Debug.WriteLine($"Ayarlar: {settings.FontSize}px, Opaklık: {settings.Opacity}");
            System.Diagnostics.Debug.WriteLine($"==========================");
        }

        public void Hide()
        {
            System.Diagnostics.Debug.WriteLine("Overlay gizlendi");
        }

        public void UpdateSettings(DisplaySettings settings)
        {
            System.Diagnostics.Debug.WriteLine($"Overlay ayarları güncellendi: {settings.FontSize}px");
        }
    }
}