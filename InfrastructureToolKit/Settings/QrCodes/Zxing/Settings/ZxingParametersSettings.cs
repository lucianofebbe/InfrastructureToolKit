using SkiaSharp;

namespace InfrastructureToolKit.Settings.QrCodes.Zxing.Settings
{
    public record ZxingParametersSettings
    {
        public string Content { get; set; }
        public string FilePath { get; set; }
        public SKBitmap Logo { get; set; }
    }
}
