namespace PST.Models
{
    public class DisplaySettings
    {
        public double FontSize { get; set; } = 14;
        public double Opacity { get; set; } = 0.8;
        public string FontFamily { get; set; } = "Arial";
        public string TextColor { get; set; } = "#FFFFFF";
        public string BackgroundColor { get; set; } = "#000000";

        public static DisplaySettings Load()
        {
            return new DisplaySettings();
        }

        public void Save()
        {
            // Kalıcı kaydetme işlemi
        }
    }
}