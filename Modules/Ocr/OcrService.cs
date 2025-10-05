using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace PST.Modules.Ocr
{
    public class ModuleManager
    {
        public async Task<string> ExtractTextAsync(Bitmap image)
        {
            try
            {
                // Basit simülasyon - sonra Tesseract entegre edeceğiz
                await Task.Delay(500);
                return "Bu OCR ile çıkarılan simüle edilmiş metindir.\nGerçek OCR implementasyonu eklenecek.";
            }
            catch (Exception ex)
            {
                throw new Exception($"OCR hatası: {ex.Message}");
            }
        }

        public List<string> GetAvailableLanguages()
        {
            return new List<string> { "English", "Turkish", "German", "French" };
        }

        public void SetActiveLanguage(string language)
        {
            // Dil ayarını kaydet
            System.Diagnostics.Debug.WriteLine($"Aktif OCR dili: {language}");
        }
    }
}