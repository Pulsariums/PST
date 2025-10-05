using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PST.Models;
using PST.Services;
using PST.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PST.Views.Pages
{
    public partial class FocusSettingsPage : UserControl
    {
        private ObservableCollection<CaptureRegion> _regions = new ObservableCollection<CaptureRegion>();
        private AppSettings _settings = new AppSettings();

        public FocusSettingsPage()
        {
            InitializeComponent();
            Debug.WriteLine("🎯 Bölge Yönetimi Sayfası Yüklendi!");

            LoadSettings();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            // Elementleri manuel olarak bul
            var regionsControl = this.FindControl<ItemsControl>("RegionsItemsControl");
            var noRegionsText = this.FindControl<TextBlock>("NoRegionsText");
            var autoCaptureCheck = this.FindControl<CheckBox>("AutoCaptureCheckBox");
            var intervalTextBox = this.FindControl<TextBox>("CaptureIntervalTextBox");

            if (regionsControl != null) regionsControl.ItemsSource = _regions;
            UpdateUI();
        }

        private void LoadSettings()
        {
            try
            {
                _settings = SettingsManager.LoadSettings();
                _regions = new ObservableCollection<CaptureRegion>(_settings.CaptureRegions);

                var regionsControl = this.FindControl<ItemsControl>("RegionsItemsControl");
                if (regionsControl != null) regionsControl.ItemsSource = _regions;

                var autoCaptureCheck = this.FindControl<CheckBox>("AutoCaptureCheckBox");
                var intervalTextBox = this.FindControl<TextBox>("CaptureIntervalTextBox");

                if (autoCaptureCheck != null)
                    autoCaptureCheck.IsChecked = _settings.AutoStartEnabled;
                if (intervalTextBox != null)
                    intervalTextBox.Text = _settings.CaptureInterval.ToString();

                Debug.WriteLine($"📁 {_regions.Count} bölge yüklendi");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Ayarlar yüklenirken hata: {ex.Message}");
            }
        }

        private void SaveSettings()
        {
            try
            {
                _settings.CaptureRegions = _regions.ToList();

                var autoCaptureCheck = this.FindControl<CheckBox>("AutoCaptureCheckBox");
                var intervalTextBox = this.FindControl<TextBox>("CaptureIntervalTextBox");

                if (autoCaptureCheck != null)
                    _settings.AutoStartEnabled = autoCaptureCheck.IsChecked ?? false;

                if (intervalTextBox != null && int.TryParse(intervalTextBox.Text, out int interval))
                    _settings.CaptureInterval = interval;

                SettingsManager.SaveSettings(_settings);
                Debug.WriteLine($"💾 Ayarlar kaydedildi: {_regions.Count} bölge");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Ayarlar kaydedilirken hata: {ex.Message}");
            }
        }

        private void UpdateUI()
        {
            var noRegionsText = this.FindControl<TextBlock>("NoRegionsText");
            var regionsControl = this.FindControl<ItemsControl>("RegionsItemsControl");

            if (noRegionsText != null)
                noRegionsText.IsVisible = _regions.Count == 0;
            if (regionsControl != null)
                regionsControl.IsVisible = _regions.Count > 0;
        }

        private async void OnAddRegionClicked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("➕ Yeni Bölge Ekle butonuna tıklandı!");

            try
            {
                var selectorWindow = new RegionSelectorWindow();
                var mainWindow = (Window)this.VisualRoot;
                var result = await selectorWindow.ShowDialog<Rect?>(mainWindow);

                if (result.HasValue && result.Value.Width > 0 && result.Value.Height > 0)
                {
                    var region = result.Value;

                    var captureRegion = new CaptureRegion
                    {
                        Name = $"Bölge {_regions.Count + 1}",
                        X = (int)region.X,
                        Y = (int)region.Y,
                        Width = (int)region.Width,
                        Height = (int)region.Height
                    };

                    _regions.Add(captureRegion);
                    SaveSettings();
                    UpdateUI();

                    Debug.WriteLine($"✅ Yeni bölge eklendi: {captureRegion}");
                }
                else
                {
                    Debug.WriteLine("❌ Bölge ekleme iptal edildi");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Bölge ekleme hatası: {ex.Message}");
            }
        }

        private void OnDeleteRegionClicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string regionId)
            {
                var regionToRemove = _regions.FirstOrDefault(r => r.Id == regionId);
                if (regionToRemove != null)
                {
                    _regions.Remove(regionToRemove);
                    SaveSettings();
                    UpdateUI();

                    Debug.WriteLine($"🗑️ Bölge silindi: {regionToRemove.Name}");
                }
            }
        }

        private void OnSaveSettingsClicked(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            Debug.WriteLine("✅ Ayarlar kaydedildi!");
        }

        private async void OnExportSettingsClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var mainWindow = (Window)this.VisualRoot;
                await SettingsManager.ExportSettingsAsync(mainWindow, _settings);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Dışa aktarma hatası: {ex.Message}");
            }
        }

        private async void OnImportSettingsClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var mainWindow = (Window)this.VisualRoot;
                var importedSettings = await SettingsManager.ImportSettingsAsync(mainWindow);

                if (importedSettings != null)
                {
                    _settings = importedSettings;
                    _regions = new ObservableCollection<CaptureRegion>(_settings.CaptureRegions);

                    var regionsControl = this.FindControl<ItemsControl>("RegionsItemsControl");
                    if (regionsControl != null) regionsControl.ItemsSource = _regions;

                    SaveSettings();
                    UpdateUI();

                    Debug.WriteLine($"✅ Ayarlar içe aktarıldı: {_regions.Count} bölge");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ İçe aktarma hatası: {ex.Message}");
            }
        }
    }
}