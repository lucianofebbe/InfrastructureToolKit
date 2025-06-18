using InfrastructureToolKit.Barcodes.ZXing.ZXing;
using InfrastructureToolKit.Interfaces.Barcodes.ZXing.ZXing;
using InfrastructureToolKit.Interfaces.Barcodes.ZXing.ZXingFactory;
using InfrastructureToolKit.Settings.Barcodes.ZXing.Settings;

namespace InfrastructureToolKit.Barcodes.ZXing.ZXingFactory
{
    public class ZXingFactory : IZXingFactory
    {
        public async Task<IZXing> Create(ZxingSettings settings)
        {
            IZXing result = new Zxing(settings);
            return result;
        }
    }
}
