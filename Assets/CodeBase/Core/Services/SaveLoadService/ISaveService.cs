using CodeBase.Core.Infrastructure.SceneManagement.Services;

namespace CodeBase.Core.Services.SaveLoadService
{
    public interface ISaveService : IService
    {
        void SaveProgress();
    }
}