using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Diagnostics;

namespace PST.Views.Windows
{
    public partial class RegionSelectorWindow : Window
    {
        public Rect SelectedRegion { get; private set; }

        public RegionSelectorWindow()
        {
            InitializeComponent();
            Debug.WriteLine("🪟 RegionSelectorWindow açıldı!");
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Pencerenin ekrandaki konumunu ve boyutunu al
                var screenPosition = this.Position;
                var screenSize = new Size(this.Bounds.Width, this.Bounds.Height);

                SelectedRegion = new Rect(screenPosition.X, screenPosition.Y, screenSize.Width, screenSize.Height);

                Debug.WriteLine($"💾 KAYDEDİLEN BÖLGE: X={SelectedRegion.X}, Y={SelectedRegion.Y}, Width={SelectedRegion.Width}, Height={SelectedRegion.Height}");

                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Kaydetme hatası: {ex.Message}");
            }
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("❌ Seçim iptal edildi");
            SelectedRegion = default;
            Close();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Debug.WriteLine("⎋ ESC ile pencere kapatıldı");
                SelectedRegion = default;
                Close();
            }
            base.OnKeyDown(e);
        }
    }
}