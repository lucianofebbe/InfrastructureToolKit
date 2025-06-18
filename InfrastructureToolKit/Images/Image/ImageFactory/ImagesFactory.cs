using InfrastructureToolKit.Interfaces.Images.Image.Image;
using InfrastructureToolKit.Interfaces.Images.Image.ImageFactory;
using InfrastructureToolKit.Settings.Images.Settings;

namespace InfrastructureToolKit.Images.Image.ImageFactory
{
    public class ImagesFactory : IImagesFactory
    {
        public async Task<IImages> Create(ImagesSettings settings)
        {
            IImages result = new Image.Images(settings);
            return result;
        }
    }
}
