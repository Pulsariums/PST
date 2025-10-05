using PST.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PST.Services
{
    public class AutoCaptureService
    {
        private Timer _captureTimer;
        private AppSettings _settings;

        public bool IsRunning { get; private set; }

        public event Action<CaptureRegion, string> OnTextCaptured;

        public void Start()
        {
            _settings = SettingsManager.LoadSettings();

            if (_settings.AutoStartEnabled && _settings.CaptureRegions.Any(r => r.IsEnabled))
            {
                _captureTimer = new Timer(async _ => await CaptureAllRegions(), null, 0, _settings.CaptureInterval);
                IsRunning = true;
                Debug.WriteLine("🚀 Otomatik yakalama başlatıldı!");
            }
        }

        public void Stop()
        {
            _captureTimer?.Dispose();
            IsRunning = false;
            Debug.WriteLine("⏹️ Otomatik yakalama durduruldu!");
        }

        private async Task CaptureAllRegions()
        {
            if (_settings?.CaptureRegions == null) return;

            var enabledRegions = _settings.CaptureRegions.Where(r => r.IsEnabled).ToList();

            Debug.WriteLine($"🔍 {enabledRegions.Count} aktif bölge taranıyor...");

            foreach (var region in enabledRegions)
            {
                try
                {
                    // OCR işlemi (şimdilik simüle)
                    var recognizedText = await SimulateOCR(region);

                    // Event tetikle
                    OnTextCaptured?.Invoke(region, recognizedText);

                    Debug.WriteLine($"📝 Bölge '{region.Name}': {recognizedText}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"❌ Bölge '{region.Name}' işlenirken hata: {ex.Message}");
                }
            }
        }

        private async Task<string> SimulateOCR(CaptureRegion region)
        {
            await Task.Delay(100);
            return $"Simüle OCR - {region.Name} ({region.Width}x{region.Height})";
        }
    }
}