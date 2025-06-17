using InfrastructureToolKit.Settings.Images.Settings;
using Interfaces.Images.Image.Image;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InfrastructureToolKit.Images.Image.Image
{
    public class Images : IImages
    {
        private readonly ImagesSettings settings;

        public Images(ImagesSettings settings)
        {
            this.settings = settings;
        }

        public virtual async Task<System.Drawing.Image?> ResizeAndTransformAsync(ImagesParametersSettings parameters)
        {
            return await Task.Run(() =>
            {
                if (parameters.Image == null || settings == null)
                    return null;

                float scaleX = (float)settings.TargetWidth / parameters.Image.Width;
                float scaleY = (float)settings.TargetHeight / parameters.Image.Height;
                float scale = settings.Fit
                    ? Math.Min(scaleX, scaleY)
                    : Math.Max(scaleX, scaleY);

                int finalWidth = settings.MaintainAspectRatio ? (int)(parameters.Image.Width * scale) : settings.TargetWidth;
                int finalHeight = settings.MaintainAspectRatio ? (int)(parameters.Image.Height * scale) : settings.TargetHeight;

                var bmp = new Bitmap(finalWidth, finalHeight);
                bmp.SetResolution(parameters.Image.HorizontalResolution, parameters.Image.VerticalResolution);

                using var graphics = Graphics.FromImage(bmp);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(Color.Transparent);

                var matrix = new Matrix();

                matrix.Translate(finalWidth / 2f, finalHeight / 2f);

                if (settings.RotateDegrees != 0)
                    matrix.Rotate(settings.RotateDegrees);

                float flipX = 1, flipY = 1;
                switch (settings.Flip)
                {
                    case FlipMode.Horizontal:
                        flipX = -1;
                        break;
                    case FlipMode.Vertical:
                        flipY = -1;
                        break;
                    case FlipMode.Both:
                        flipX = -1;
                        flipY = -1;
                        break;
                }

                matrix.Scale(scale * flipX, scale * flipY);
                matrix.Translate(-parameters.Image.Width / 2f, -parameters.Image.Height / 2f);

                graphics.Transform = matrix;
                graphics.DrawImage(parameters.Image, new Rectangle(0, 0, parameters.Image.Width, parameters.Image.Height));

                return bmp;
            });
        }

        public virtual async Task<System.Drawing.Image?> ResizeAndTransformFromPathAsync(ImagesParametersSettings parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.ImagePath) || !File.Exists(parameters.ImagePath))
                return null;

            using var source = System.Drawing.Image.FromFile(parameters.ImagePath);
            return await ResizeAndTransformAsync(parameters);
        }

        public virtual async Task<System.Drawing.Image?> ResizeAndTransformFromBytesAsync(ImagesParametersSettings parameters)
        {
            if (parameters.ImageBytes == null || parameters.ImageBytes.Length == 0)
                return null;

            using var ms = new MemoryStream(parameters.ImageBytes);
            using var source = System.Drawing.Image.FromStream(ms);
            return await ResizeAndTransformAsync(parameters);
        }
    }
}
