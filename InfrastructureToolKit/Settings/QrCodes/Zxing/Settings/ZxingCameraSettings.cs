using ZXing;

namespace InfrastructureToolKit.Settings.QrCodes.Zxing.Settings
{
    public record ZxingCameraSettings
    {
        public IList<BarcodeFormat>? BarcodeFormats { get; set; } = null;
    }
}
