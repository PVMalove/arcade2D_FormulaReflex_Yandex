using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.Core.Services.SaveLoadService
{
    public interface ISaveService : IService
    {
        void SaveProgress();
    }
}