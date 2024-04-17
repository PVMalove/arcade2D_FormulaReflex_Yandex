using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.UI.LoadingCurtain
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private CanvasGroup сurtain;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            Debug.Log("LoadingCurtain -> Show");
            gameObject.SetActive(true);
            сurtain.alpha = 1;
        }
        
        public void Hide()
        {
            StartCoroutine(FadeIn());
            Debug.Log("LoadingCurtain -> Hide");
        }

        private void OnDisable()
        {
            StopCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            float fadeStep = 0.05f;
    
            yield return new WaitForSeconds(0.5f); // Equivalent to UniTask.Delay(500)
            while (сurtain.alpha > 0)
            {
                сurtain.alpha -= fadeStep;
                yield return new WaitForSeconds(0.05f); // Equivalent to UniTask.Delay(50)
            }
    
            gameObject.SetActive(false);
        }
    }
}