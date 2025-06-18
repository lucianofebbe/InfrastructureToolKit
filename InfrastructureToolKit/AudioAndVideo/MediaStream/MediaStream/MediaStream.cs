using InfrastructureToolKit.Interfaces.AudioAndVideo.MediaStream;
using InfrastructureToolKit.Settings.AudioAndVideo.MediaStream.Settings;

namespace InfrastructureToolKit.AudioAndVideo.MediaStream.MediaStream
{
    public class MediaStream : IMediaStream, IChat
    {
        private MediaStreamSettings settings;

        private readonly Dictionary<string, List<ParticipantSettings>> _roomsParticipants = new();

        public MediaStream(MediaStreamSettings settings)
        {
            this.settings = settings;
        }

        public virtual async Task SendMessageAsync(ChatSettings settings)
        {
            throw new NotImplementedException();
        }

        public virtual async Task StartRecordingAsync(MediaStreamSettings settings)
        {
            throw new NotImplementedException();
        }

        public virtual async Task StartStreamAsync(MediaStreamSettings settings)
        {
            if (!_roomsParticipants.ContainsKey(settings.RoomName))
            {
                _roomsParticipants[settings.RoomName] = new List<ParticipantSettings>();
            }

            // Adiciona transmissor como participante
            //_roomsParticipants[settings.RoomName].Add(new Participant(settings.TransmitterName));

            //await _mediaServer.StartStreamAsync(settings.RoomName);

            //if (settings.EnableRecording && !string.IsNullOrEmpty(settings.RecordingPath))
            //{
            //    await _mediaServer.StartRecordingAsync(settings.RoomName, settings.RecordingPath);
            //}
            throw new NotImplementedException();
        }

        public virtual async Task StopRecordingAsync(MediaStreamSettings settings)
        {
            throw new NotImplementedException();
        }

        public virtual async Task StopStreamAsync(MediaStreamSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
