namespace InfrastructureToolKit.Settings.AudioAndVideo.MediaStream.Settings
{
    public record ChatSettings
    {
        public string SenderName { get; set; }
        public string Message { get; set; }
        public Action<string, string>? OnMessageReceived { get; set; }
    }
}
