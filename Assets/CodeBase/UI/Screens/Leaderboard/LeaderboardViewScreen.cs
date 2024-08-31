using CodeBase.UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;
using YG;


namespace CodeBase.UI.Screens.Leaderboard
{
    public class LeaderboardViewScreen : ScreenBase<ILeaderboardPresenter>
    {
        [SerializeField] private Button closeScreenButton;
        [SerializeField] private GameObject content;

        [Space] [Header("Leaderboard")] 
        [SerializeField] private Button yandexRegistrationButton;
        [SerializeField] private GameObject yandexRegistrationObject;
        [SerializeField] private Transform rootSpawnPlayersData;

        private ILeaderboardPresenter presenter;

        protected override void Initialize(ILeaderboardPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();

            if (presenter is null) return;
            content.SetActive(true);
            closeScreenButton.onClick.AddListener(CloseScreen);
            
            if (!YandexGame.auth)
            {
                yandexRegistrationObject.SetActive(true);
                yandexRegistrationButton.onClick.AddListener(RegistrationOnClick);
                return;
            }
            
            presenter.Subscribe();
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            
            if (presenter is null) return;
            content.SetActive(false);
            closeScreenButton.onClick.RemoveListener(CloseScreen);
            yandexRegistrationButton.onClick.RemoveListener(RegistrationOnClick);
            presenter.Unsubscribe();
        }

        public void SetImageCarList()
        {       
            for (int i = 0; i < rootSpawnPlayersData.childCount; i++)
            {
                rootSpawnPlayersData.GetChild(i).GetComponent<CarView>().SetSprite(presenter.ThisPlayerDataRank == i
                    ? presenter.SelectedCar
                    : presenter.RandomSprites[i]);
            }
        }

        private void RegistrationOnClick()
        {
            YandexGame.AuthDialog();
            presenter.RestartGame();
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