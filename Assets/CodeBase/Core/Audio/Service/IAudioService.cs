using System;
using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.Core.Audio.Service
{
    public interface IAudioService : IService
    {
        event Action<bool> OnChangedMuteMusicState;
        
        // SourceAudio MusicSourceAudio { get; }
        // SourceAudio FXSourceAudio { get; }

        void ChangeVolume(float value);
        void ToggleMusic(bool isOn);
        void ToggleEffects(bool isOn);
        event Action<bool> OnChangedMuteFXState;
    }
}