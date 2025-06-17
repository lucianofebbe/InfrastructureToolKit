namespace InfrastructureToolKit.Settings.AudioAndVideo.MediaStream.Settings
{
    public record ReceptionSettings
    {
        public string RoomName { get; set; } = string.Empty;
        public string ViewerName { get; set; } = string.Empty;
    }
}
