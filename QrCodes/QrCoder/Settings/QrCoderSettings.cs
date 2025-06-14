using System.Drawing;

namespace InfrastructureToolKit.QrCodes.QrCoder.Settings
{
    public record QrCoderSettings
    {
        public OutputFormat OutputFormat { get; set; }
        public int PixelsPerModule { get; set; } = 25;
        public int IconSizePercent { get; set; } = 15;
        public int IconBorderWidth { get; set; } = 6;
        public bool DrawQuietZones { get; set; } = true;
        public Payload? Payload { get; set; }
        public ECCLevel? ECCLevel { get; set; }
        public EciMode? EciMode { get; set; }
        public Color ColorFront { get; set; } = Color.Black;
        public Color ColorBackground { get; set; } = Color.White;
        public bool ForceUtf8 { get; set; } = false;
        public bool Utf8BOM { get; set; } = false;
        public int RequestedVersion { get; set; } = -1;
    }

    public enum OutputFormat { Png, Svg }

    public enum ECCLevel
    {
        Default = -1,
        L,
        M,
        Q,
        H
    }

    public enum EciMode
    {
        Default = 0,
        Iso8859_1 = 3,
        Iso8859_2 = 4,
        Utf8 = 26
    }

    public enum Payload
    {
        BezahlCode,
        BitcoinAddress,
        BitcoinCashAddress,
        BitcoinLikeCryptoCurrencyAddress,
        Bookmark,
        CalendarEvent,
        ContactData,
        Geolocation,
        Girocode,
        LitecoinAddress,
        Mail,
        MMS,
        MoneroTransaction,
        OneTimePassword,
        PhoneNumber,
        RussiaPaymentOrder,
        ShadowSocksConfig,
        SkypeCall,
        SlovenianUpnQr,
        SMS,
        SwissQrCode,
        Url,
        WhatsAppMessage,
        WiFi
    }
}
