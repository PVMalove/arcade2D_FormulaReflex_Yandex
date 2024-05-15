using CodeBase.UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens.Car
{
    public class CarViewScreen : ScreenBase<ICarPresenter>
    {
        [SerializeField] private Image CarSprite;
        
        private ICarPresenter presenter;
        
        protected override void Initialize(ICarPresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
            presenter.Subscribe();
            presenter.ChangedSelectedCar += OnSelectedCarChanged;
            OnSelectedCarChanged(presenter.SelectedCar);
        }

        private void OnSelectedCarChanged(Sprite view) => 
            CarSprite.sprite = view;
    }
}