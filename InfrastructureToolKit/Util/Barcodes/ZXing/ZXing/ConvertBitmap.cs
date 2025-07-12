using InfrastructureToolKit.Interfaces.Util.Barcodes.ZXing.ZXing;
using InfrastructureToolKit.Settings.Util.Barcodes.ZXing;
using SkiaSharp;

namespace InfrastructureToolKit.Util.Barcodes.ZXing.ZXing
{
    public class ConvertBitmap : IConvertBitmap
    {
        private ConvertBitmapSettings Settings;
        public ConvertBitmap(ConvertBitmapSettings settings)
        {
            Settings = settings;
        }

        public async Task<string> BitmapToBase64(SKBitmap bitmap)
        {
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode((SKEncodedImageFormat)Settings.EncodedImage, Settings.Quality);
            return Convert.ToBase64String(data.ToArray());
        }

        public async Task<(SKBitmap Bitmap, string Extension)> Base64ToBitmap(string content)
        {
            var cleanBase64 = content.Contains(",") ? content.Split(',')[1] : content;
            byte[] imageBytes = Convert.FromBase64String(cleanBase64);

            using var codecStream = new SKMemoryStream(imageBytes);
            using var codec = SKCodec.Create(codecStream);
            var extension = GetExtensionFromFormat(codec.EncodedFormat);

            using var decodeStream = new SKMemoryStream(imageBytes);
            var bitmap = SKBitmap.Decode(decodeStream) ?? throw new Exception("Falha ao decodificar imagem");

            return (bitmap, extension);
        }

        private static string GetExtensionFromFormat(SKEncodedImageFormat format)
        {
            return format switch
            {
                SKEncodedImageFormat.Jpeg => ".jpg",
                SKEncodedImageFormat.Png => ".png",
                SKEncodedImageFormat.Gif => ".gif",
                SKEncodedImageFormat.Webp => ".webp",
                SKEncodedImageFormat.Bmp => ".bmp",
                SKEncodedImageFormat.Wbmp => ".wbmp",
                SKEncodedImageFormat.Heif => ".heif",
                SKEncodedImageFormat.Ico => ".ico",
                SKEncodedImageFormat.Ktx => ".ktx",
                SKEncodedImageFormat.Pkm => ".pkm",
                SKEncodedImageFormat.Astc => ".astc",
                SKEncodedImageFormat.Dng => ".dng",
                _ => ".img"
            };
        }
    }
}
