using System;
using System.Collections.Generic;
using CodeBase.Core.Audio.Service;
using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Infrastructure.Factories;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.StaticData.Infrastructure;
using CodeBase.Core.StaticData.UI.Shop;
using CodeBase.UI.HUD.Service;
using CodeBase.UI.Screens.Service;
using UnityEngine;
using YG;

namespace CodeBase.UI.HUD.SettingBar
{
    public sealed class SettingBarPresenter : ISettingBarPresenter
    {
        public event Action<bool> OnChangedFXState;
        
        private readonly IPersistentProgressService progressService;
        private readonly IAssetProvider assetProvider;
        private readonly IGameFactory gameFactory;
        private readonly IHUDService hudService;
        private readonly IScreenService screenService;
        private readonly IAudioService audioService;

        public float AudioVolume { get; private set; }
        public bool MusicOn { get; private set; }
        public bool EffectsOn { get; private set; }

        public SettingBarPresenter(IPersistentProgressService progressService, IAssetProvider assetProvider,
            IGameFactory gameFactory, IHUDService hudService, IScreenService screenService, IAudioService audioService)
        {
            this.progressService = progressService;
            this.assetProvider = assetProvider;
            this.gameFactory = gameFactory;
            this.hudService = hudService;
            this.screenService = screenService;
            this.audioService = audioService;
        }
        
        public void Enable()
        {
            audioService.OnChangedMuteFXState += OnMuteStateFXChanged;
        }

        public void Disable()
        {
            audioService.OnChangedMuteFXState -= OnMuteStateFXChanged;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            AudioVolume = progressService.GetProgress().AudioControlData.AudioVolume;
            EffectsOn = progressService.GetProgress().AudioControlData.EffectsOn;
            UpdateSettingState();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progressService.GetProgress().AudioControlData.AudioVolume = AudioVolume;
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

            PlayerCarData playerCarData = new PlayerCarData(
                new List<CarViewType> { newSaveData.DefaultCarViewType },
                newSaveData.DefaultCarViewType
            );

            CoinData coinData = new CoinData(
                newSaveData.CoinsAmount
            );

            BestTimeData bestTimeData = new BestTimeData(
                newSaveData.BestTime
            );
            
            AudioControlData audioControl = new AudioControlData(
                newSaveData.AudioVolume,
                newSaveData.EffectsOn
            );

            PlayerProgress progress = new PlayerProgress(
                playerCarData,
                coinData,
                bestTimeData,
                audioControl);

            progressService.Initialize(progress);
            
            foreach (IProgressReader progressReader in gameFactory.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            foreach (IProgressReader progressReader in hudService.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            foreach (IProgressReader progressReader in screenService.ProgressReaders)
                progressReader.LoadProgress(progressService.GetProgress());
            
            Debug.Log("Init new player progress");
        }
    }
}