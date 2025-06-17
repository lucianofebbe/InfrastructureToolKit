using InfrastructureToolKit.Settings.Barcodes.ZXing.Settings;
using SkiaSharp;

namespace Interfaces.Barcodes.ZXing.ZXing
{
    public interface IZXing
    {
        Task<SKBitmap> GenerateAsync(ZxingParametersSettings parameters);
        Task SaveAsync(ZxingParametersSettings parameters);
    }
}
