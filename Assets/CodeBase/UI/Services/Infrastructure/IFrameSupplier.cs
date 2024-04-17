using CodeBase.Core.Infrastructure.SceneManagement.Services;

namespace CodeBase.UI.Services.Infrastructure
{
    public interface IFrameSupplier<in TKey, TValue> : IService where TValue : UnityFrame
    { 
        TValue LoadFrame(TKey key);

        void UnloadFrame(TValue frame);
    }
}