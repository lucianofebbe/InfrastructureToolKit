using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ZXing;

namespace InfrastructureToolKit.Settings.Barcodes.ZXing.Settings
{
    public class ZxingSettings
    {
        public Format BarCodeFormat { get; set; } = Format.EAN_13;
        public EncodedImageFormat EncodedImage { get; set; } = EncodedImageFormat.Png;
        public int Width { get; set; } = 400;
        public int Height { get; set; } = 400;
        public int Margin { get; set; } = 10;
        public int Quality { get; set; } = 100;
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Format
    {
        AZTEC = 1,
        CODABAR = 2,
        CODE_39 = 4,
        CODE_93 = 8,
        CODE_128 = 0x10,
        DATA_MATRIX = 0x20,
        EAN_8 = 0x40,
        EAN_13 = 0x80,
        ITF = 0x100,
        MAXICODE = 0x200,
        PDF_417 = 0x400,
        QR_CODE = 0x800,
        RSS_14 = 0x1000,
        RSS_EXPANDED = 0x2000,
        UPC_A = 0x4000,
        UPC_E = 0x8000,
        UPC_EAN_EXTENSION = 0x10000,
        MSI = 0x20000,
        PLESSEY = 0x40000,
        IMB = 0x80000,
        PHARMA_CODE = 0x100000,
        All_1D = 0xF1DE
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EncodedImageFormat
    {
        Bmp = 0,
        Gif = 1,
        Ico = 2,
        Jpeg = 3,
        Png = 4,
        Wbmp = 5,
        Webp = 6,
        Pkm = 7,
        Ktx = 8,
        Astc = 9,
        Dng = 10,
        Heif = 11,
        Avif = 12,
        Jpegxl = 13,
    }
}
