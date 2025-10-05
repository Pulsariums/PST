using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using PST.Views.Pages;

namespace PST.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowHomePage(); // Varsayýlan sayfa
        }

        private void ShowHomePage()
        {
            if (MainContentControl != null)
                MainContentControl.Content = new HomePage();
        }

        private void OnHomePageClicked(object sender, RoutedEventArgs e)
        {
            if (MainContentControl != null)
                MainContentControl.Content = new HomePage();
        }

        private void OnOcrSettingsClicked(object sender, RoutedEventArgs e)
        {
            if (MainContentControl != null)
                MainContentControl.Content = new OcrSettingsPage();
        }

        private void OnFocusSettingsClicked(object sender, RoutedEventArgs e)
        {
            if (MainContentControl != null)
                MainContentControl.Content = new FocusSettingsPage();
        }

        private void OnTranslationSettingsClicked(object sender, RoutedEventArgs e)
        {
            if (MainContentControl != null)
                MainContentControl.Content = new TranslationSettingsPage();
        }

        private void OnDisplaySettingsClicked(object sender, RoutedEventArgs e)
        {
            if (MainContentControl != null)
                MainContentControl.Content = new DisplaySettingsPage();
        }

        private void OnGeneralSettingsClicked(object sender, RoutedEventArgs e)
        {
            if (MainContentControl != null)
                MainContentControl.Content = new GeneralSettingsPage();
        }

        private void TitleBar_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                BeginMoveDrag(e);
            }
        }
    }
}