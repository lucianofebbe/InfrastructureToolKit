using SkiaSharp;

namespace InfrastructureToolKit.Settings.Barcodes.ZXing.Settings
{
    public class ZxingParametersSettings
    {
        public string Content { get; set; }
        public SKBitmap Bitmap { get; set; }
        public string FilePath { get; set; }
        public int Quality { get; set; } = 100;
        public SKEncodedImageFormat Format { get; set; } = SKEncodedImageFormat.Png;
    }
}
