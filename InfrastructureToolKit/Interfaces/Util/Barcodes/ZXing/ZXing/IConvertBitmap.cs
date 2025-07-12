using SkiaSharp;

namespace InfrastructureToolKit.Interfaces.Util.Barcodes.ZXing.ZXing
{
    public interface IConvertBitmap
    {
        Task<string> BitmapToBase64(SKBitmap bitmap);
        Task<(SKBitmap Bitmap, string Extension)> Base64ToBitmap(string content);
    }
}
