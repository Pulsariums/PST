using PST.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace PST.Services
{
    public static class SettingsManager
    {
        private static readonly string SettingsPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PST", "settings.json");

        public static AppSettings LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    var json = File.ReadAllText(SettingsPath);
                    return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"⚠️ Ayarlar yüklenirken hata: {ex.Message}");
            }

            return new AppSettings();
        }

        public static void SaveSettings(AppSettings settings)
        {
            try
            {
                var directory = Path.GetDirectoryName(SettingsPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsPath, json);

                System.Diagnostics.Debug.WriteLine("💾 Ayarlar kaydedildi!");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Ayarlar kaydedilirken hata: {ex.Message}");
            }
        }

        // Import/Export metodları
        public static async Task ExportSettingsAsync(Window parentWindow, AppSettings settings)
        {
            try
            {
                var filePicker = parentWindow.StorageProvider;
                var file = await filePicker.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions
                {
                    Title = "Ayarları Dışa Aktar",
                    SuggestedFileName = $"PST_Settings_{DateTime.Now:yyyyMMdd_HHmmss}.json",
                    FileTypeChoices = new[]
                    {
                        new FilePickerFileType("JSON Dosyası")
                        {
                            Patterns = new[] { "*.json" }
                        }
                    }
                });

                if (file != null)
                {
                    var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                    using (var stream = await file.OpenWriteAsync())
                    using (var writer = new StreamWriter(stream))
                    {
                        await writer.WriteAsync(json);
                    }
                    System.Diagnostics.Debug.WriteLine($"📤 Ayarlar dışa aktarıldı: {file.Name}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Dışa aktarma hatası: {ex.Message}");
            }
        }

        public static async Task<AppSettings> ImportSettingsAsync(Window parentWindow)
        {
            try
            {
                var filePicker = parentWindow.StorageProvider;
                var files = await filePicker.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
                {
                    Title = "Ayarları İçe Aktar",
                    AllowMultiple = false,
                    FileTypeFilter = new[]
                    {
                        new FilePickerFileType("JSON Dosyası")
                        {
                            Patterns = new[] { "*.json" }
                        }
                    }
                });

                if (files.Count > 0 && files[0] != null)
                {
                    await using var stream = await files[0].OpenReadAsync();
                    using var reader = new StreamReader(stream);
                    var json = await reader.ReadToEndAsync();
                    var settings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();

                    System.Diagnostics.Debug.WriteLine($"📥 Ayarlar içe aktarıldı: {files[0].Name}");
                    return settings;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ İçe aktarma hatası: {ex.Message}");
            }

            return new AppSettings();
        }
    }
}