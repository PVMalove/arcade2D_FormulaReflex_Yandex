using UnityEngine;

namespace CodeBase.Core.Services.PoolService
{
    public class PoolFactory : IPoolFactory
    {
        public TComponent Create<TComponent>(GameObject prefab, Vector3 position, Transform parent = null)
            where TComponent : MonoBehaviour
        {
            GameObject instance = Object.Instantiate(prefab, position, Quaternion.identity, parent);

            TComponent component = instance.GetComponent<TComponent>();

            if (component == null)
                component = instance.AddComponent<TComponent>();

            return component;
        }
    }
}