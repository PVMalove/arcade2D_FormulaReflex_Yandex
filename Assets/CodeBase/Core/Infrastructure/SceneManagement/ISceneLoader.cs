using System.Threading.Tasks;
using CodeBase.Core.Infrastructure.AssetManagement;

namespace CodeBase.Core.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        Task Load(SceneNames nextScene);
    }
}