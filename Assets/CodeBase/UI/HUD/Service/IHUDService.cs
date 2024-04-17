using System.Collections.Generic;
using CodeBase.Core.Infrastructure.SceneManagement.Services;
using CodeBase.Core.Services.ProgressService;
using CodeBase.UI.HUD.BuildInfo;

namespace CodeBase.UI.HUD.Service
{
    public interface IHUDService : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressWriters { get; }
        
        void ShowBuildInfo(BuildInfoConfig config);
        void ShowSettingBar();
        void Cleanup();
    }
}