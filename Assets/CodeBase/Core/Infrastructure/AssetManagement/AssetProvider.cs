
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
        
        public TAsset Load<TAsset>(string path) where TAsset : Object
        {
            var request = Resources.Load<TAsset>(path);

            return request;
        }
        
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}