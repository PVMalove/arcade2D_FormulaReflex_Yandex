using System;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens.Shop.Item
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Text requiredCoinsText;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Image lockImage;
        [SerializeField] private GameObject selectLabel;
        [SerializeField] private GameObject coinsContainer;
        
        [SerializeField] private Button selectButton;
        [SerializeField] private Button buyButton;
        
        public bool IsSelected => selectLabel.gameObject.activeSelf;

        private IPersistentProgressService progressService;
        private int requiredCoinsAmount;

        private void Awake()
        {
            progressService = AllServices.Container.Single<IPersistentProgressService>();
        }

        private void OnEnable()
        {
            progressService.CoinsAmountChanged += OnPlayerProgressChanged;
        }

        private void OnDisable()
        {
            buyButton.onClick.RemoveAllListeners();
            selectButton.onClick.RemoveAllListeners();
            progressService.CoinsAmountChanged -= OnPlayerProgressChanged;
        }

        public void SetItem(Sprite image, int requiredCoins,
            Action onBuyButtonClicked, Action onSelectButtonClicked)
        {
            itemIcon.sprite = image;
            requiredCoinsText.text = requiredCoins.ToString();
            
            buyButton.onClick.AddListener(() =>
            {
                onBuyButtonClicked?.Invoke();
                Unlock();
            });
            
            selectButton.onClick.AddListener(() =>
            {
                onSelectButtonClicked?.Invoke();
                Select();
            });

            requiredCoinsAmount = requiredCoins;
            OnPlayerProgressChanged();
        }

        public void Lock()
        {
            coinsContainer.SetActive(true);
            //lockImage.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
        }

        public void Unlock()
        {
            coinsContainer.SetActive(false);
            //lockImage.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
        }

        public void Select()
        {
            selectButton.gameObject.SetActive(false);
            selectLabel.SetActive(true);
        }

        public void Unselect()
        {
            selectButton.gameObject.SetActive(true);
            selectLabel.SetActive(false);
        }
        
        private void OnPlayerProgressChanged()
        {
            bool isCoinsEnough = progressService.IsCoinsEnoughFor(requiredCoinsAmount);
            buyButton.interactable = isCoinsEnough;
        }
    }
}