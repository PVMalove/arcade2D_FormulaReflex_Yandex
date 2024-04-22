using System.Collections.Generic;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Core.Services.ServiceLocator;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressWriters { get; }

        void Cleanup();
    }
}