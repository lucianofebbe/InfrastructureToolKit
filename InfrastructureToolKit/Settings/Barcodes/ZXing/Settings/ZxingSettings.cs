using ZXing;

namespace InfrastructureToolKit.Settings.Barcodes.ZXing.Settings
{
    public class ZxingSettings
    {
        public BarcodeFormat Format { get; set; } = BarcodeFormat.EAN_13;
        public int Width { get; set; } = 400;
        public int Height { get; set; } = 400;
        public int Margin { get; set; } = 10;
    }
}
