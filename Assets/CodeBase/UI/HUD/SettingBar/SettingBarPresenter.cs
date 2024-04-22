using System;
using CodeBase.Core.Audio.Service;
using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.StaticData.Infrastructure;
using CodeBase.UI.HUD.Service;
using UnityEngine;
using YG;

namespace CodeBase.UI.HUD.SettingBar
{
    public sealed class SettingBarPresenter : ISettingBarPresenter
    {
        public event Action<bool> OnChangedMusicState;
        public event Action<bool> OnChangedFXState;
        
        private readonly IPersistentProgressService progressService;
        private readonly IAssetProvider assetProvider;
        private readonly IGameFactory gameFactory;
        private readonly IHUDService hudService;
        private readonly IAudioService audioService;

        public float AudioVolume { get; private set; }
        public bool MusicOn { get; private set; }
        public bool EffectsOn { get; private set; }

        public SettingBarPresenter(IPersistentProgressService progressService, IAssetProvider assetProvider,
            IGameFactory gameFactory, IHUDService hudService, IAudioService audioService)
        {
            this.progressService = progressService;
            this.assetProvider = assetProvider;
            this.gameFactory = gameFactory;
            this.hudService = hudService;
            this.audioService = audioService;
        }
        
        public void Enable()
        {
            audioService.OnChangedMuteMusicState += OnMuteStateMusicChanged;
            audioService.OnChangedMuteFXState += OnMuteStateFXChanged;
        }

        public void Disable()
        {
            audioService.OnChangedMuteMusicState -= OnMuteStateMusicChanged;
            audioService.OnChangedMuteFXState -= OnMuteStateFXChanged;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            AudioVolume = progressService.GetProgress().AudioControlData.AudioVolume;
            MusicOn = progressService.GetProgress().AudioControlData.MusicOn;
            EffectsOn = progressService.GetProgress().AudioControlData.EffectsOn;
            UpdateSettingState();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progressService.GetProgress().AudioControlData.AudioVolume = AudioVolume;
            progressService.GetProgress().AudioControlData.MusicOn = MusicOn;
            progressService.GetProgress().AudioControlData.EffectsOn = EffectsOn;
        }

        public void ToggleMusic()
        {
            MusicOn = !MusicOn;
            audioService.ToggleMusic(MusicOn);
        }

        public void ToggleEffects()
        {
            EffectsOn = !EffectsOn;
            audioService.ToggleEffects(EffectsOn);
        }
        
        public void ResetProgress()
        {
            NewProgress();
        }

        private void OnMuteStateMusicChanged(bool state) => 
            OnChangedMusicState?.Invoke(state);

        private void OnMuteStateFXChanged(bool state) => 
            OnChangedFXState?.Invoke(state);

        private void UpdateSettingState() 
        {
            audioService.ChangeVolume(AudioVolume);
            audioService.ToggleMusic(MusicOn);
            audioService.ToggleEffects(EffectsOn);
        }

        private void NewProgress()
        {
            Debug.Log("Reset player progress");
            YandexGame.ResetSaveProgress();
            FirstSaveData newSaveData = assetProvider.Load<FirstSaveData>(InfrastructurePath.NewSaveDataPath);

            AudioControlData audioControl = new AudioControlData(
                newSaveData.AudioVolume, 
                newSaveData.MusicOn,
                newSaveData.EffectsOn
            );
            
            CoinData coinData = new CoinData(
                newSaveData.CoinsAmount
            );

            PlayerProgress progress = new PlayerProgress(
                audioControl,
                coinData);

            progressService.Initialize(progress);
            
            foreach (IProgressReader progressReader in gameFactory.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            foreach (IProgressReader progressReader in hudService.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            
            Debug.Log("Init new player progress");
        }
    }
}