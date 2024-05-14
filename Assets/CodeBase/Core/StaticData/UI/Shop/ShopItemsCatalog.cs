using System.Collections.Generic;
using System.Linq;
using CodeBase.Core.Infrastructure.Extentions;
using UnityEngine;

namespace CodeBase.Core.StaticData.UI.Shop
{
    [CreateAssetMenu(fileName = "ShopItemCatalog", menuName = "Configs/UI/SkinsShop/ShopItemCatalog", order = 0)]
    public class ShopItemsCatalog : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<CarViewType, CarShopItemConfig> carItems; 
        public Dictionary<CarViewType, CarShopItemConfig> CarItems => carItems;
    }
}