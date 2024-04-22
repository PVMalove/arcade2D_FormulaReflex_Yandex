using System.Threading.Tasks;
using CodeBase.Core.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Core.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async Task Load(SceneNames sceneName)
        {
            string nextScene = sceneName.ToString();
            
            if (SceneManager.GetActiveScene().name == nextScene) return;

            Application.backgroundLoadingPriority = ThreadPriority.High;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);

            while (!asyncOperation!.isDone) 
                await Task.Yield();
        }
    }
}