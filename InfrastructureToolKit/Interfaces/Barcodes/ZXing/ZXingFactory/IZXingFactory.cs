using InfrastructureToolKit.Settings.Barcodes.ZXing.Settings;
using Interfaces.Barcodes.ZXing.ZXing;

namespace Interfaces.Barcodes.ZXing.ZXingFactory
{
    public interface IZXingFactory
    {
        Task<IZXing> Create(ZxingSettings settings);
    }
}
