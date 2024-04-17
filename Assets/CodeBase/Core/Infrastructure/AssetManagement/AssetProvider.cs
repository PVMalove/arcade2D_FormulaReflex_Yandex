
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Core.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public async Task<TAsset> LoadAsync<TAsset>(string path) where TAsset : Object
        {
            var request = Resources.LoadAsync<TAsset>(path);
            while (!request.isDone)
            {
                await Task.Yield();
            }
            return request.asset as TAsset;
        }
        
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}