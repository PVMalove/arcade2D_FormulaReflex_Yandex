using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.Core.Services.RestartGameService
{
    public interface IRestartGameService : IService
    {
        void RestartGame();
    }
}