using InfrastructureToolKit.Settings.Images.Settings;
using Interfaces.Images.Image.Image;

namespace Interfaces.Images.Image.ImageFactory
{
    public interface IImagesFactory
    {
        Task<IImages> Create(ImagesSettings settings);
    }
}
