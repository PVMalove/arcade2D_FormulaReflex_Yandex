using CodeBase.Core.StaticData.UI.Shop;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Core.StaticData.Infrastructure
{
    [CreateAssetMenu(fileName = "FirstSaveData", menuName = "Configs/Infrastructure/FirstSaveData")]
    public class FirstSaveData : ScriptableObject
    {
        [SerializeField] private CarType defaultCarType;
        [SerializeField] private int coinsAmount;
        [SerializeField] private float bestTime;
        [SerializeField] private float audioVolume;
        [SerializeField] private bool effectsOn;

        public CarType DefaultCarType => defaultCarType;
        public int CoinsAmount => coinsAmount;
        public float BestTime => bestTime;
        public float AudioVolume => audioVolume;
        public bool EffectsOn => effectsOn;
    }
}