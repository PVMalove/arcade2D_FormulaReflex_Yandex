using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.Screens.Base
{
    public class ScreenBase<TInitializeData> : UnityFrame
    {
        private void Awake() => 
            OnAwake();

        public void Show(TInitializeData with)
        {
            Initialize(with);
            gameObject.SetActive(true);
        }

        protected void Hide() => 
            gameObject.SetActive(false);

        private void OnEnable()
        {
            SubscribeUpdates();
        }
        private void OnDisable()
        {
            UnsubscribeUpdates();
        }

        private void OnDestroy() => 
            Cleanup();

        protected virtual void OnAwake() { }
        protected virtual void Initialize(TInitializeData with){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void UnsubscribeUpdates() { }
        protected virtual void Cleanup(){}
    }
}