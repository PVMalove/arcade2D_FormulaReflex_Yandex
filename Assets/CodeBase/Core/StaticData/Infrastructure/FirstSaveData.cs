using CodeBase.Core.StaticData.UI.Shop;
using UnityEngine;

namespace CodeBase.Core.StaticData.Infrastructure
{
    [CreateAssetMenu(fileName = "FirstSaveData", menuName = "Configs/Infrastructure/FirstSaveData")]
    public class FirstSaveData : ScriptableObject
    {
        [SerializeField] private CarViewType defaultCarViewType;
        [SerializeField] private int coinsAmount;
        [SerializeField] private float bestTime;
        [SerializeField] private float audioVolume;
        [SerializeField] private bool effectsOn;

        public CarViewType DefaultCarViewType => defaultCarViewType;
        public int CoinsAmount => coinsAmount;
        public float BestTime => bestTime;
        public float AudioVolume => audioVolume;
        public bool EffectsOn => effectsOn;
    }
}