using CodeBase.Core.Audio.Service;
using CodeBase.Core.Infrastructure.SceneManagement;
using CodeBase.Core.Infrastructure.States.Infrastructure;
using CodeBase.Core.Infrastructure.UI.LoadingCurtain;
using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.Core.Infrastructure.States
{
    public class Game
    {
        private readonly GlobalStateMachine _stateMachine;

        public Game(LoadingCurtain curtain, IAudioService audioService) => 
            _stateMachine = new GlobalStateMachine(new SceneLoader(), curtain,audioService, AllServices.Container);

        public void Start() => _stateMachine.Start();
    }
}