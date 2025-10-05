using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PST.Models;

namespace PST.Modules.Translation
{
    public class TranslationService
    {
        public async Task<string> TranslateAsync(string text, string targetLang)
        {
            try
            {
                // Basit simülasyon - sonra Google API entegre edeceğiz
                await Task.Delay(300);

                return targetLang.ToLower() switch
                {
                    "tr" => $"[Türkçe] Bu bir çeviri simülasyonudur. Orijinal: {text}",
                    "en" => $"[English] This is a translation simulation. Original: {text}",
                    _ => $"[{targetLang}] Translation simulation. Original: {text}"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Çeviri hatası: {ex.Message}");
            }
        }

        public List<Language> GetSupportedLanguages()
        {
            return new List<Language>
    {
        new() { Code = "en", Name = "English" },
        new() { Code = "tr", Name = "Turkish" },
        new() { Code = "de", Name = "German" },
        new() { Code = "fr", Name = "French" }
    };
        }

        public bool TestConnection()
        {
            // Bağlantı testi - her zaman başarılı simüle ediyoruz
            return true;
        }
    }
}