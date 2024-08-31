using System;
using System.Collections;
using UnityEngine;
using YG;


namespace CodeBase.UI.Screens.Service
{
    public class TimerAds : MonoBehaviour
    {
        public event Action EndShowAdCountdown;
        
        [SerializeField] private GameObject secondsPanelObject;
        [SerializeField] private GameObject[] secondObjects;
        
        private bool isAdActive;
        private int objSecCounter;

        public bool IsAdActive => isAdActive;

        private void OnEnable()
        {
            if (secondsPanelObject)
                secondsPanelObject.SetActive(false);

            foreach (GameObject gmObj in secondObjects)
                gmObj.SetActive(false);
        }

        public void CheckTimerAd()
        {
            isAdActive = YandexGame.timerShowAd > YandexGame.Instance.infoYG.fullscreenAdInterval
                         && Time.timeScale != 0;
        }
        public void StartAdCountdown() => 
            StartCoroutine(ShowAdCountdown());

        private IEnumerator ShowAdCountdown()
        {
            if (secondsPanelObject)
                secondsPanelObject.SetActive(true);

            foreach (GameObject gmObj in secondObjects)
            {
                gmObj.SetActive(true);
                yield return new WaitForSecondsRealtime(0.7f);
                gmObj.SetActive(false);
            }

            YandexGame.FullscreenShow();

            yield return new WaitUntil(() => YandexGame.nowFullAd);
            
            secondsPanelObject.SetActive(false);
            EndShowAdCountdown?.Invoke();
        }
    }
}