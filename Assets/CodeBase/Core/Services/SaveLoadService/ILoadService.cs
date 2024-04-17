using System.Threading.Tasks;
using CodeBase.Core.Data;
using CodeBase.Core.Infrastructure.SceneManagement.Services;

namespace CodeBase.Core.Services.SaveLoadService
{
    public interface ILoadService : IService
    {
        Task<PlayerProgress> LoadProgress();
    }
}