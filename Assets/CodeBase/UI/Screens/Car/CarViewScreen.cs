using CodeBase.UI.Screens.Base;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;


namespace CodeBase.UI.Screens.Car
{
    public class CarViewScreen : ScreenBase<ICarPresenter>
    {
        [SerializeField] private Image carSprite;
        [SerializeField] private AnimationCar animationCar;

        [CanBeNull] 
        private ICarPresenter presenter;
        
        protected override void Initialize(ICarPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            if (presenter is null) return;
            presenter.Subscribe();
            presenter.ChangedSelectedCar += OnSelectedCarChanged;
            presenter.ResetCarAnimation += OnResetAnimation;
            presenter.CoinsAmount += ChangeCoinAmountText;
            presenter.PlayCarAnimation += OnPlayAnimation;
            OnSelectedCarChanged(presenter.SelectedCar);
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            if (presenter is null) return;
            presenter.ResetCarAnimation -= OnResetAnimation;
            presenter.CoinsAmount -= ChangeCoinAmountText;
            presenter.PlayCarAnimation -= OnPlayAnimation;
            presenter.Unsubscribe();
        }

        private void OnSelectedCarChanged(Sprite view) => 
            carSprite.sprite = view;

        private void ChangeCoinAmountText(int amount) => 
            animationCar.SetAmountCoins(amount);

        private void OnPlayAnimation(string type) => 
            animationCar.PlayAnimation(type);

        private void OnResetAnimation()
        {
            animationCar.ResetAnimation();
        }
    }
}