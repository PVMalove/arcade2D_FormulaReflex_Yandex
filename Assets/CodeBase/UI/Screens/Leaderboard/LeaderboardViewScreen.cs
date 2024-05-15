using CodeBase.UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace CodeBase.UI.Screens.Leaderboard
{
    public class LeaderboardViewScreen : ScreenBase<ILeaderboardPresenter>
    {
        [SerializeField] private Button closeScreenButton;
        
        [Space] [Header("Leaderboard")] 
        [SerializeField] private Button yandexRegistrationButton;
        [SerializeField] private GameObject yandexRegistrationObject;
        [SerializeField] private Transform rootSpawnPlayersData;
        
        private ILeaderboardPresenter presenter;

        protected override void Initialize(ILeaderboardPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;

            if (YandexGame.auth) return;
            yandexRegistrationObject.SetActive(true);
            yandexRegistrationButton.onClick.AddListener(RegistrationOnClick);
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            closeScreenButton.onClick.AddListener(CloseScreen);
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            closeScreenButton.onClick.RemoveListener(CloseScreen);
            yandexRegistrationButton.onClick.RemoveListener(RegistrationOnClick);
        }

        public void SetImageCarList()
        {
            for (int i = 0; i < rootSpawnPlayersData.childCount; i++)
            {
                rootSpawnPlayersData.GetChild(i).GetComponent<CarView>().SetSprite(presenter.RandomSprites[i]);
            }
        }

        private void RegistrationOnClick()
        {
            YandexGame.AuthDialog();
            CloseScreen();
        }

        private void CloseScreen()
        {
            Hide();
            for (int i = 0; i < rootSpawnPlayersData.childCount; i++)
            {
                Destroy(rootSpawnPlayersData.GetChild(i).gameObject);
            }
        }
    }
}