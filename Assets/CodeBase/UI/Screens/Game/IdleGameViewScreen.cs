using System.Globalization;
using CodeBase.Core.StaticData.UI.Shop;
using CodeBase.UI.Screens.Base;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;
using YG;


namespace CodeBase.UI.Screens.Game
{
    public class IdleGameViewScreen : ScreenBase<IGamePresenter>
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button openSkinsShopButton;
        [SerializeField] private Button openLeaderboardButton;
        [SerializeField] private Button openCoinShopButton;
        [SerializeField] private Text bestTimeText;
        [SerializeField] private GameObject bestTimeObject;
        [SerializeField] private Text coinsAmountText;
        [SerializeField] private RectTransform targetCoins;
        [SerializeField] private GameObject notifyObject;

        private IGamePresenter presenter;

        protected override void Initialize(IGamePresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            if (presenter is null) return;
            presenter.Subscribe();

            presenter.ChangedCoinsAmount += OnCoinsAmountChanged;
            startGameButton.onClick.AddListener(OnStartGame);
            openSkinsShopButton.onClick.AddListener(OnOpenSkinsShop);
            openLeaderboardButton.onClick.AddListener(OnOpenLeaderboard);
            openCoinShopButton.onClick.AddListener(OnOpenCoinShop);
            
            // YandexGame.GetUnprocessedPurchasesEvent += PurchaseRecovery;
            // YandexGame.CheckUnprocessedPurchases();
            
            BestTimeChanged();
            OnCoinsAmountChanged();
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            if (presenter is null) return;
            presenter.ChangedCoinsAmount -= OnCoinsAmountChanged;
            startGameButton.onClick.RemoveListener(OnStartGame);
            openSkinsShopButton.onClick.RemoveListener(OnOpenSkinsShop);
            openLeaderboardButton.onClick.RemoveListener(OnOpenLeaderboard);
            openCoinShopButton.onClick.RemoveListener(OnOpenCoinShop);
            //YandexGame.GetUnprocessedPurchasesEvent -= PurchaseRecovery;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            presenter.Unsubscribe();
        }

        private void OnStartGame()
        {
            Hide();
            presenter.StartGame();
        }

        private void OnOpenSkinsShop()
        {
            presenter.OpenShop();
        }

        private void OnOpenLeaderboard() =>
            presenter.OpenLeaderboard();

        private void OnOpenCoinShop()
        {
            presenter.OpenCoinShop();
        }

        private void BestTimeChanged()
        {
            if (presenter.BestTime == "00.000")
            {
                bestTimeObject.gameObject.SetActive(false);
            }
            else
            {
                bestTimeObject.gameObject.SetActive(true);
                bestTimeText.text = presenter.BestTime;
            }
        }

        private void OnCoinsAmountChanged()
        {
            int startCoinValue = int.Parse(coinsAmountText.text);
            int endCoinValue = int.Parse(presenter.CoinsAmount);

            Sequence.Create()
                .Group(Tween.Scale(targetCoins, startValue: 0.9f,
                    endValue: 1.2f,
                    duration: 0.25f,
                    Ease.InOutQuad))
                .Group(Tween.Custom(coinsAmountText, startCoinValue, endCoinValue, 1, UpdateCoinsText))
                .Chain(Tween.Scale(targetCoins, startValue: 1.2f,
                    endValue: 1f,
                    duration: 0.25f,
                    Ease.InOutQuad));

            CheckNotify();
        }

        private void UpdateCoinsText(Text target, float newValue) =>
            coinsAmountText.text = Mathf.Floor(newValue).ToString(CultureInfo.InvariantCulture);

        private void CheckNotify()
        {
            notifyObject.SetActive(false);
            for (int i = 0; i < presenter.SkinsData.Count; i++)
            {
                if (presenter.SkinsData.TryGetValue((CarType)i, out CarStoreItemConfig value))
                {
                    if (int.TryParse(presenter.CoinsAmount, out int coinsAmount))
                        if (coinsAmount >= value.RequiredCoins)
                        {
                            if (i == 0) continue;
                            if (presenter.IsPlayerOwnCar((CarType)i)) continue;
                            notifyObject.SetActive(true);
                            return;
                        }
                }
            }
        }
        
        private void PurchaseRecovery(string id)
        {
            presenter.Log($"PurchaseRecovery: id - {id}", this);
            if (id == "AddCoin")
            {
                presenter.Log("Purchase restore active",this);
                presenter.ShowRestorePurchase();
            }
        }
    }
}