using CodeBase.Core.Services.PoolService;
using CodeBase.Core.Services.ServiceLocator;
using CodeBase.Core.StaticData.UI.Shop;

namespace CodeBase.Core.Services.StaticDataService
{
    public interface IStaticDataService : IService
    {
        void Initialize();
        ShopItemsCatalog ShopItemsCatalog { get; } 
        PoolObjectConfig GetPoolConfigByType(PoolObjectType type);
    }
}