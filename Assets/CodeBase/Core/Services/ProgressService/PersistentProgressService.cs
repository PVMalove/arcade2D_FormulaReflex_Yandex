using CodeBase.Core.Data;

namespace CodeBase.Core.Services.ProgressService
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private PlayerProgress playerProgress;

        public void Initialize(PlayerProgress progress)
        {
            playerProgress = progress;
        }

        public PlayerProgress GetProgress() => playerProgress;
    }
}