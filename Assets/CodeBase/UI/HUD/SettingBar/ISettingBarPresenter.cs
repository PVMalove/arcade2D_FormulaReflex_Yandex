using System;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.UI.HUD.SettingBar
{
    public interface ISettingBarPresenter : IProgressSaver, IService
    {
        event Action<bool> OnChangedFXState;
        
        float AudioVolume { get; }
        bool MusicOn { get; }
        bool EffectsOn { get; }
        void ToggleMusic();
        void ToggleEffects();
        void Enable();
        void Disable();
        void ResetProgress();
    }
}