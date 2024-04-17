using CodeBase.Core.Infrastructure.States;
using UnityEngine;
using YG;

namespace CodeBase.Core.GameStarter
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper bootstrapperPrefab;
        [SerializeField] private YandexGame yandex;

        private void OnEnable() => 
            YandexGame.GetDataEvent += LoadCompleteYandexSDK;

        private void LoadCompleteYandexSDK()
        {
            if (YandexGame.SDKEnabled)
            {
                Debug.Log("Load complete yandex SDK");
                GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();
                if (bootstrapper == null)
                    Instantiate(bootstrapperPrefab);

                Destroy(gameObject);
            }
        }

        private void Awake()
        {
            YandexGame loadingYandexSDK = FindObjectOfType<YandexGame>();
            if (loadingYandexSDK == null)
                Instantiate(yandex);
        }

        private void OnDisable() => 
            YandexGame.GetDataEvent -= LoadCompleteYandexSDK;
    }
}