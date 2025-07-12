using InfrastructureToolKit.Interfaces.Util.Barcodes.ZXing.ZXing;
using InfrastructureToolKit.Interfaces.Util.Barcodes.ZXing.ZXingFactory;
using InfrastructureToolKit.Settings.Util.Barcodes.ZXing;
using InfrastructureToolKit.Util.Barcodes.ZXing.ZXing;

namespace InfrastructureToolKit.Util.Barcodes.ZXing.ZXingFactory
{
    public class ConvertBitmapFactory : IConvertBitmapFactory
    {
        public async Task<IConvertBitmap> Create(ConvertBitmapSettings settings)
        {
            return new ConvertBitmap(settings);
        }
    }
}
