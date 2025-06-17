using InfrastructureToolKit.Settings.Barcodes.ZXing.Settings;
using Interfaces.Barcodes.ZXing.ZXing;
using SkiaSharp;
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp.Rendering;

namespace InfrastructureToolKit.Barcodes.ZXing.ZXing
{
    public class Zxing : IZXing
    {
        private ZxingSettings settings;

        public Zxing(ZxingSettings settings)
        {
            this.settings = settings;
        }

        public virtual async Task<SKBitmap> GenerateAsync(ZxingParametersSettings parameters)
        {
            var writer = new BarcodeWriter<SKBitmap>
            {
                Format = settings.Format,
                Options = new EncodingOptions
                {
                    Width = settings.Width,
                    Height = settings.Height,
                    Margin = settings.Margin
                },
                Renderer = new SKBitmapRenderer()
            };

            return writer.Write(parameters.Content);
        }

        public virtual async Task SaveAsync(ZxingParametersSettings parameters)
        {
            using var imagem = SKImage.FromBitmap(parameters.Bitmap);
            using var dados = imagem.Encode(parameters.Format, parameters.Quality);
            using var fs = File.OpenWrite(parameters.FilePath);
            dados.SaveTo(fs);
        }
    }
}
