using UnityEngine;

namespace CodeBase.Core.StaticData.Game
{
    [CreateAssetMenu(fileName = "BolideData", menuName = "Configs/Game/BolideData", order = 0)]
    public class BolideData: ScriptableObject
    {
        [SerializeField] private Sprite[] imagesContainer;
        public Sprite[] ImagesContainer => imagesContainer;
    }
}