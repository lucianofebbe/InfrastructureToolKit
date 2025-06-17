using SkiaSharp;
using ZXing;

namespace InfrastructureToolKit.Settings.QrCodes.Zxing.Settings
{
    public record ZxingSettings
    {
        public BarcodeFormat BarcodeFormat { get; set; } = BarcodeFormat.QR_CODE;
        public int Width { get; set; } = 400;
        public int Height { get; set; } = 400;
        public int Margin { get; set; } = 1;
        public SKEncodedImageFormat SKEncodedImageFormat { get; set; } = SKEncodedImageFormat.Png;
        public int ImageQuality { get; set; } = 100;
        public SKBitmap? SKBitmap { get; set; } = null;
        public IList<BarcodeFormat>? BarcodeFormats { get; set; } = null;
    }
}
