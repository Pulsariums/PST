using System;

namespace PST.Services
{
    public static class ErrorHandler
    {
        public static void HandleOcrError(Exception ex)
        {
            ShowUserFriendlyMessage($"OCR Hatası: {ex.Message}");
        }

        public static void HandleTranslationError(Exception ex)
        {
            ShowUserFriendlyMessage($"Çeviri Hatası: {ex.Message}");
        }

        public static void ShowUserFriendlyMessage(string message)
        {
            // TODO: Gerçek dialog gösterimi
            System.Diagnostics.Debug.WriteLine($"Kullanıcı Mesajı: {message}");
        }
    }
}