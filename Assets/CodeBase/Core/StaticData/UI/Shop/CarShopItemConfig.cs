using UnityEngine;

namespace CodeBase.Core.StaticData.UI.Shop
{
    [CreateAssetMenu(fileName = "CarItem", menuName = "Configs/UI/ItemsShop/CarItem", order = 1)]
    public class CarShopItemConfig : ScriptableObject
    {
        [field: SerializeField] public CarViewType Type { get; set; }
        [field: SerializeField] public Sprite CarView { get; private set; }
        [field: SerializeField, Range(0, 1000)] public int RequiredCoins { get; private set; }
    }
}