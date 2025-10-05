using PST.Modules.ScreenCapture;
using PST.Modules.Translation;
using PST.Modules.Overlay;

namespace PST.Modules
{
    public static class ModuleManager
    {
        public static ScreenCaptureService ScreenCapture { get; }
        public static TranslationService Translation { get; }
        public static OverlayService Overlay { get; }

        static ModuleManager()
        {
            ScreenCapture = new ScreenCaptureService();
            Translation = new TranslationService();
            Overlay = new OverlayService();
        }
    }
}