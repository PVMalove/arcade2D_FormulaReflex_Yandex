using System;
using Plugins.Audio.Core;
using UnityEngine;


namespace CodeBase.Core.Audio.Service
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        public event Action<bool> OnChangedMuteMusicState;
        public event Action<bool> OnChangedMuteFXState;
        
        [SerializeField] private SourceAudio fx_f_motor_AudioSource;
        [SerializeField] private SourceAudio fx_f_start_AudioSource;
        [SerializeField] private SourceAudio fx_lp_AudioSource;

        private float volumeCurrent;

        public SourceAudio FX_f_motor_SourceAudio =>
            fx_f_motor_AudioSource;
        
        public SourceAudio FX_f_start_SourceAudio =>
            fx_f_start_AudioSource;
        
        public SourceAudio FX_lp_SourceAudio =>
            fx_lp_AudioSource;
        

        private void Awake() => 
            DontDestroyOnLoad(gameObject);
        
        public void ChangeVolume(float value) =>
            AudioListener.volume = value;

        public void ToggleEffects(bool isOn)
        {
            fx_f_motor_AudioSource.Mute = !isOn;
            fx_lp_AudioSource.Mute = !isOn;
            fx_f_start_AudioSource.Mute = !isOn;
            OnChangedMuteFXState?.Invoke(!fx_f_motor_AudioSource.Mute);
        }
    }
}