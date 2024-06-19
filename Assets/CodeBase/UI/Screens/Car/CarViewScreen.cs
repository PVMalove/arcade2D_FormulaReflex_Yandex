using CodeBase.UI.Screens.Base;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace CodeBase.UI.Screens.Car
{
    public class CarViewScreen : ScreenBase<ICarPresenter>
    {
        [SerializeField] private Image carSprite;
        [SerializeField] private AnimationCar animationCar;
        
        
        private ICarPresenter presenter;
        
        protected override void Initialize(ICarPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            presenter.Subscribe();
            presenter.ChangedSelectedCar += OnSelectedCarChanged;

            OnSelectedCarChanged(presenter.SelectedCar);
        }
        
        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            presenter.PlayAnimationEndGame += OnPlayAnimation;
            presenter.ResetAnimationEndGame += OnResetAnimation;
            //AnimationUi.OnAnimationEnded += OnResetAnimation;
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            presenter.PlayAnimationEndGame -= OnPlayAnimation;
            presenter.ResetAnimationEndGame -= OnResetAnimation;
        }

        private void OnSelectedCarChanged(Sprite view) => 
            carSprite.sprite = view;

        private void OnPlayAnimation()
        {
            animationCar.PlayAnimation();

            //AnimationUi.Play();
        }

        private void OnResetAnimation()
        {
            animationCar.ResetAnimation();
            //AnimationUi.PreviewStart();
        }
    }
}