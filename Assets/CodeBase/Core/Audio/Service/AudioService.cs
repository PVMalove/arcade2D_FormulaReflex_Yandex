using System;
using UnityEngine;

namespace CodeBase.Core.Audio.Service
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        public event Action<bool> OnChangedMuteMusicState;
        public event Action<bool> OnChangedMuteFXState;
        
        [SerializeField] public AudioSource musicAudioSource;
        [SerializeField] public AudioSource fxAudioSource;

        private float volumeCurrent;

        // public SourceAudio MusicSourceAudio =>
        //     musicAudioSource.GetComponent<SourceAudio>();
        // public SourceAudio FXSourceAudio =>
        //     fxAudioSource.GetComponent<SourceAudio>();

        private void Awake() => 
            DontDestroyOnLoad(gameObject);
        
        public void ChangeVolume(float value) =>
            AudioListener.volume = value;

        public void ToggleMusic(bool isOn)
        {
            musicAudioSource.mute = !isOn;
            OnChangedMuteMusicState?.Invoke(!musicAudioSource.mute);
        }

        public void ToggleEffects(bool isOn)
        {
            fxAudioSource.mute = !isOn;
            OnChangedMuteFXState?.Invoke(!fxAudioSource.mute);
        }
    }
}