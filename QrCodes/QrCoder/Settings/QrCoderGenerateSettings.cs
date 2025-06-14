namespace InfrastructureToolKit.QrCodes.QrCoder.Settings
{
    public record QrCoderGenerateSettings
    {
        public string Text { get; set; }
        public string OutPut { get; set; }
        public string PathLogo { get; set; }
        public byte[]? BinaryData { get; set; }
    }
}
