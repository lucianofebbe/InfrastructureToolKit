using InfrastructureToolKit.Settings.Images.Settings;
using Interfaces.Images.Image.Image;
using Interfaces.Images.Image.ImageFactory;

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
