using InfrastructureToolKit.Interfaces.Images.Image.Image;
using InfrastructureToolKit.Settings.Images.Settings;

namespace InfrastructureToolKit.Interfaces.Images.Image.ImageFactory
{
    public interface IImagesFactory
    {
        Task<IImages> Create(ImagesSettings settings);
    }
}
