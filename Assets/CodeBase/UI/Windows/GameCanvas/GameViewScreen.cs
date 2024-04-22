using System.Collections;
using CodeBase.UI.Windows.Base;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.GameCanvas
{
    public class GameViewScreen : ScreenBase<IGamePresenter>
    {
        [SerializeField] private GameObject[] panelTrafficLights;
        [SerializeField] private float minDelay = 1f;
        [SerializeField] private float maxDelay = 2.5f;

        [SerializeField] private Button startGameButton;
        [SerializeField] private Button openSkinsShopButton;

        private IGamePresenter presenter;

        protected override void Initialize(IGamePresenter presenter)
        {
            base.Initialize(presenter);
            this.presenter = presenter;
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            startGameButton.onClick.AddListener(OnStartGame);
            openSkinsShopButton.onClick.AddListener(OnOpenSkinsShop);
        }

        protected override void UnsubscribeUpdates()
        {
            base.UnsubscribeUpdates();
            startGameButton.onClick.RemoveListener(OnStartGame);
            openSkinsShopButton.onClick.RemoveListener(OnOpenSkinsShop);
        }

        private void OnStartGame()
        {
            StartCoroutine(StartLights());
        }

        private void OnOpenSkinsShop()
        {
        }

        private IEnumerator StartLights()
        {
            foreach (GameObject panel in panelTrafficLights)
            {
                panel.SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            ClearLights();
        }

        private void ClearLights()
        {
            foreach (GameObject panel in panelTrafficLights)
            {
                panel.SetActive(false);
            }
        }
    }
}