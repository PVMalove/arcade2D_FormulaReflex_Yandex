using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.Core.Services.LogService
{
    public interface ILogService : IService
    {
        void Log(string msg, object obj);
        void LogState(string msg, object obj);
        void LogYandex(string msg, object obj);
        void LogAudio(string msg, object obj);
        void LogError(string msg);
        void LogWarning(string msg);
    }
}