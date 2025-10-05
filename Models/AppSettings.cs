using System.Collections.Generic;

namespace PST.Models
{
    public class AppSettings
    {
        // Genel Ayarlar
        public string DefaultLanguage { get; set; } = "tr";
        public bool AutoStart { get; set; } = false;

        // Bölge Yönetimi
        public List<CaptureRegion> CaptureRegions { get; set; } = new List<CaptureRegion>();
        public bool AutoStartEnabled { get; set; } = false;
        public int CaptureInterval { get; set; } = 5000;

        // OCR Ayarları
        public string OcrEngine { get; set; } = "Tesseract";
        public string OcrLanguage { get; set; } = "eng";

        // Çeviri Ayarları
        public string TranslationService { get; set; } = "Google";
        public string TargetLanguage { get; set; } = "tr";

        // Görüntüleme Ayarları
        public DisplaySettings DisplaySettings { get; set; } = new DisplaySettings();

        // Pencere Konumu
        public WindowPosition MainWindowPosition { get; set; } = new WindowPosition();
    }

    public class WindowPosition
    {
        public double X { get; set; } = 100;
        public double Y { get; set; } = 100;
        public double Width { get; set; } = 800;
        public double Height { get; set; } = 600;
    }
}