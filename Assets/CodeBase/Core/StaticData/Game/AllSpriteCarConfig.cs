using UnityEngine;

namespace CodeBase.Core.StaticData.Game
{
    [CreateAssetMenu(fileName = "AllSpriteCarConfig", menuName = "Configs/Game/AllSpriteCarConfig", order = 0)]
    public class AllSpriteCarConfig: ScriptableObject
    {
        [SerializeField] private Sprite[] imagesContainer;
        public Sprite[] ImagesContainer => imagesContainer;
    }
}