using UnityEngine;

namespace CodeBase.Core.StaticData.Game
{
    [CreateAssetMenu(fileName = "AllCarViewConfig", menuName = "Configs/Game/AllCarViewConfig", order = 0)]
    public class AllCarViewConfig: ScriptableObject
    {
        [SerializeField] private Sprite[] imagesContainer;
        public Sprite[] ImagesContainer => imagesContainer;
    }
}