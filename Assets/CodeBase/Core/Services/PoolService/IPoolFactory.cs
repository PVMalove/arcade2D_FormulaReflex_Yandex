using CodeBase.Core.Services.ServiceLocator;
using UnityEngine;

namespace CodeBase.Core.Services.PoolService
{
    public interface IPoolFactory : IService
    { 
        TComponent Create<TComponent>(GameObject prefab, Vector3 position, Transform parent = null) 
            where TComponent : MonoBehaviour;
    }
}