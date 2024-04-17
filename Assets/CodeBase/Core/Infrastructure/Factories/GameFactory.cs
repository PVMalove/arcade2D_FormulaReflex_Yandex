using System.Collections.Generic;
using CodeBase.Core.Services.ProgressService;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; } = new List<IProgressSaver>();

        private readonly IPersistentProgressService progressService;


        public GameFactory(IPersistentProgressService progressService)
        {
            this.progressService = progressService;
        }


        private void Register(GameObject gameObject)
        {
            RegisterProgressWatchers(gameObject);
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (IProgressReader progressReader in gameObject.GetComponentsInChildren<IProgressReader>())
                RegisterProgress(progressReader);
        }

        private void RegisterProgress(IProgressReader progressReader)
        {
            if (progressReader is IProgressSaver progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }


        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}