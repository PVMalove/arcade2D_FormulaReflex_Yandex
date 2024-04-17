using System.Threading.Tasks;
using CodeBase.Core.Infrastructure.SceneManagement.Services;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        Task<TAsset> LoadAsync<TAsset>(string path) where TAsset : Object;
        GameObject Instantiate(string path);
    }
}