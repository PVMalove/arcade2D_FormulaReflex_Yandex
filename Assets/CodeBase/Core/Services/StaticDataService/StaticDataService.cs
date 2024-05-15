using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Core.Infrastructure.AssetManagement;
using CodeBase.Core.Services.LogService;
using CodeBase.Core.Services.PoolService;
using CodeBase.Core.StaticData.UI.Shop;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Core.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private readonly ILogService log;
        private Lazy<Dictionary<PoolObjectType, PoolObjectConfig>> poolObjectConfigsCache;

        public PoolObjectConfig GetPoolConfigByType(PoolObjectType type) => poolObjectConfigsCache.Value[type];
        public StoreItemsCatalog StoreItemsCatalog { get; private set; }

        public StaticDataService(ILogService log)
        {
            this.log = log;
        }

        public void Initialize()
        {
            poolObjectConfigsCache = new Lazy<Dictionary<PoolObjectType, PoolObjectConfig>>(LoadPoolConfigs);
            LoadSkinsItemConfig();
            log.Log("Static data loaded", this);
        }

        private Dictionary<PoolObjectType, PoolObjectConfig> LoadPoolConfigs()
        {
            List<PoolObjectConfig> configsList = GetConfigs<PoolObjectConfig>(InfrastructurePath.ConfigPath);
            return configsList.ToDictionary(config => config.Type, config => config);
        }

        private void LoadSkinsItemConfig()
        {
            List<StoreItemsCatalog> configs = GetConfigs<StoreItemsCatalog>(InfrastructurePath.ConfigPath);
            if (configs.Count > 0)
                StoreItemsCatalog = configs.First();
            else
                log.LogError("There are no shop items config founded!");
        }

        private List<TConfig> GetConfigs<TConfig>(string label) where TConfig : class
        {
            return GetAssetsListByLabel<TConfig>(label);
        }

        private List<TConfig> GetAssetsListByLabel<TConfig>(string label) where TConfig : class
        {
            Object[] assets = Resources.LoadAll(label, typeof(TConfig));
            return assets.OfType<TConfig>().ToList();
        }
    }
}