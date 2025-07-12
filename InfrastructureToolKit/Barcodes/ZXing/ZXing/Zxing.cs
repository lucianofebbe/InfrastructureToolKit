using InfrastructureToolKit.Interfaces.Barcodes.ZXing.ZXing;
using InfrastructureToolKit.Settings.Barcodes.ZXing.Settings;
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

        public virtual async Task<SKBitmap> GenerateAsync(GenerateZxingParameters parameters)
        {
            var writer = new BarcodeWriter<SKBitmap>
            {
                Format = (BarcodeFormat)settings.BarCodeFormat,
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

        public virtual async Task SaveAsync(SaveZxingParameters parameters)
        {
            using var imagem = SKImage.FromBitmap(parameters.Bitmap);
            using var dados = imagem.Encode((SKEncodedImageFormat)settings.EncodedImage, settings.Quality);
            using var fs = File.OpenWrite(parameters.FilePath);
            dados.SaveTo(fs);
        }
    }
}
