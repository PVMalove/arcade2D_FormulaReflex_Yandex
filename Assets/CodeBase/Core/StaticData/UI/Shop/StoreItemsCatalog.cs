using System.Collections.Generic;
using CodeBase.Core.Infrastructure.Extentions;
using UnityEngine;

namespace CodeBase.Core.StaticData.UI.Shop
{
    [CreateAssetMenu(fileName = "StoreItemsCatalog", menuName = "Configs/UI/CarStore/StoreItemsCatalog", order = 0)]
    public class StoreItemsCatalog : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<CarType, CarStoreItemConfig> carItems; 
        public Dictionary<CarType, CarStoreItemConfig> CarItems => carItems;
    }
}