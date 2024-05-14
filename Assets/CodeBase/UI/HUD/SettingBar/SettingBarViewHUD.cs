using CodeBase.UI.HUD.Base;
using CodeBase.UI.HUD.SettingBar.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.HUD.SettingBar
{
    public sealed class SettingBarViewHUD : HUDBase<ISettingBarPresenter>
    {
        [Header("Setting panel")] 
        [SerializeField] private Button settingButton;
        
        [SerializeField] private GameObject settingsObject;
        [SerializeField] private SoundButton sfxSoundButton;
        [SerializeField] private Button resetProgressButton;
        
        private ISettingBarPresenter setting;
        private VerticalLayoutGroup layoutGroup;
        
        protected override void Initialize(ISettingBarPresenter presenter)
        {
            base.Initialize(presenter);
            setting = presenter;
            setting.Enable();
            setting.OnChangedFXState += FXButtonUpdateState;
            layoutGroup = settingsObject.GetComponent<VerticalLayoutGroup>();
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            settingButton.onClick.AddListener(OpenSettingOnClick);
            sfxSoundButton.AddListener(SFXOnClick);
            resetProgressButton.onClick.AddListener(ResetProgressOnClick);
        }
        
        private void FXButtonUpdateState(bool state) => 
            sfxSoundButton.ChangeStateSoundButton(state);

        private void OpenSettingOnClick()
        {
            settingsObject.SetActive(!settingsObject.activeSelf);
        }

        private void SoundOnClick()
        {
            setting.ToggleMusic();
        }

        private void SFXOnClick()
        {
            setting.ToggleEffects();
        }

        private void ResetProgressOnClick()
        {
            setting.ResetProgress();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            setting.Disable();
            sfxSoundButton.RemoveListener(SFXOnClick);
            setting.OnChangedFXState -= FXButtonUpdateState;
        }
    }
}