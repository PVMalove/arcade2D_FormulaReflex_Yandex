using System.Threading.Tasks;
using CodeBase.Core.Data;
using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.Core.Services.SaveLoadService
{
    public interface ILoadService : IService
    {
        Task<PlayerProgress> LoadProgress();
    }
}