using InfrastructureToolKit.Settings.Barcodes.ZXing.Settings;

namespace InfrastructureToolKit.Settings.Util.Barcodes.ZXing
{
    public record ConvertBitmapSettings
    {
        public EncodedImageFormat EncodedImage { get; set; }
        public int Quality{ get; set; }
    }
}
