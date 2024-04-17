using CodeBase.Core.Audio.Service;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.SceneManagement.Services;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;

namespace CodeBase.Core.Infrastructure.States
{
    public class Game
    {
        private readonly GameStateMachine _stateMachine;

        public Game(LoadingCurtain curtain, IAudioService audioService) => 
            _stateMachine = new GameStateMachine(new SceneLoader(), curtain,audioService, AllServices.Container);

        public void Start() => _stateMachine.Start();
    }
}