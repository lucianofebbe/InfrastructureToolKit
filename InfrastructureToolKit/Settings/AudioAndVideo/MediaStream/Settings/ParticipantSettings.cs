namespace InfrastructureToolKit.Settings.AudioAndVideo.MediaStream.Settings
{
    public record ParticipantSettings
    {
        public string Name { get; set; }
        public DateTime JoinedAt { get; set; }

        public ParticipantSettings(string name)
        {
            Name = name;
            JoinedAt = DateTime.UtcNow;
        }
    }
}
