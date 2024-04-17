using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure.SceneManagement.Services;

namespace CodeBase.Core.Services.ProgressService
{
    public interface IPersistentProgressService : IService
    {
        void Initialize(PlayerProgress progress);
        PlayerProgress GetProgress();
    }
}