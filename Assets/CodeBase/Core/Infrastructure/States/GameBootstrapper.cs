using CodeBase.Core.Audio.Service;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.States
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain curtainPrefab;
        [SerializeField] private AudioService audioServicePrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(Instantiate(curtainPrefab),Instantiate(audioServicePrefab));
            _game.Start();
            DontDestroyOnLoad(this);
        }
    }
}