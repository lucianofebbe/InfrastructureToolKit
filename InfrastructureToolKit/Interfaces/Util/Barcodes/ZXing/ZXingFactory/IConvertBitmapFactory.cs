using InfrastructureToolKit.Interfaces.Util.Barcodes.ZXing.ZXing;
using InfrastructureToolKit.Settings.Util.Barcodes.ZXing;

namespace InfrastructureToolKit.Interfaces.Util.Barcodes.ZXing.ZXingFactory
{
    public interface IConvertBitmapFactory
    {
        Task<IConvertBitmap> Create(ConvertBitmapSettings settings);
    }
}
