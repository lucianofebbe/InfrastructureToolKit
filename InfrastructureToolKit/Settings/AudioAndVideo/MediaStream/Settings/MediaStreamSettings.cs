namespace InfrastructureToolKit.Settings.AudioAndVideo.MediaStream.Settings
{
    public record MediaStreamSettings
    {
        public string SenderName { get; set; }
        public string Message { get; set; }
        public string RoomName { get; set; }
        public string Path { get; set; }
    }
}
