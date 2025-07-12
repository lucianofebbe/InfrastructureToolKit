using InfrastructureToolKit.Settings.Barcodes.ZXing.Settings;
using SkiaSharp;

namespace InfrastructureToolKit.Interfaces.Barcodes.ZXing.ZXing
{
    public interface IZXing
    {
        Task<SKBitmap> GenerateAsync(GenerateZxingParameters parameters);
        Task SaveAsync(SaveZxingParameters parameters);
    }
}
