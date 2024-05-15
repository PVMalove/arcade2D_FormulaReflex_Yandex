using UnityEngine;

namespace CodeBase.Core.StaticData.UI.Shop
{
    [CreateAssetMenu(fileName = "CarItem", menuName = "Configs/UI/CarStore/CarItem", order = 1)]
    public class CarStoreItemConfig : ScriptableObject
    {
        [field: SerializeField] public CarType Type { get; set; }
        [field: SerializeField] public Sprite CarSprite { get; private set; }
        [field: SerializeField, Range(0, 1000)] public int RequiredCoins { get; private set; }
    }
}