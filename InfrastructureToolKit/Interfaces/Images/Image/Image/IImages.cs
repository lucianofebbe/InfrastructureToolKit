using InfrastructureToolKit.Settings.Images.Settings;

namespace InfrastructureToolKit.Interfaces.Images.Image.Image
{
    public interface IImages
    {
        Task<System.Drawing.Image?> ResizeAndTransformAsync(ImagesParametersSettings parameters);
        Task<System.Drawing.Image?> ResizeAndTransformFromPathAsync(ImagesParametersSettings parameters);
        Task<System.Drawing.Image?> ResizeAndTransformFromBytesAsync(ImagesParametersSettings parameters);
    }
}
