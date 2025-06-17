using InfrastructureToolKit.Barcodes.ZXing.ZXing;
using InfrastructureToolKit.Settings.Barcodes.ZXing.Settings;
using Interfaces.Barcodes.ZXing.ZXing;
using Interfaces.Barcodes.ZXing.ZXingFactory;

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
