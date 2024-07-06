using System;
using CodeBase.Core.Services.ServiceLocator;
using Plugins.Audio.Core;

namespace CodeBase.Core.Audio.Service
{
    public interface IAudioService : IService
    {
        event Action<bool> OnChangedMuteMusicState;
        event Action<bool> OnChangedMuteFXState;

        SourceAudio FX_f_motor_SourceAudio { get; }
        SourceAudio FX_lp_SourceAudio { get; }
        SourceAudio FX_f_start_SourceAudio { get; }
        void ChangeVolume(float value);
        void ToggleEffects(bool isOn);
    }
}