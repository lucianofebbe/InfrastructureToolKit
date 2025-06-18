using InfrastructureToolKit.Interfaces.Barcodes.ZXing.ZXing;
using InfrastructureToolKit.Settings.Barcodes.ZXing.Settings;

namespace InfrastructureToolKit.Interfaces.Barcodes.ZXing.ZXingFactory
{
    public interface IZXingFactory
    {
        Task<IZXing> Create(ZxingSettings settings);
    }
}
