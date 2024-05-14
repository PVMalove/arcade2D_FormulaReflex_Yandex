using UnityEngine;

namespace CodeBase.Core.Services.PoolService
{
    [CreateAssetMenu(fileName = "NewName_PoolObjectData", menuName = "Configs/Infrastructure/Pool/PoolObjectData")]
    public class PoolObjectConfig : ScriptableObject
    {
        [SerializeField] private PoolObjectType type;
        [SerializeField] private int startCapacity;
        [SerializeField] private GameObject assetReference;

        public PoolObjectType Type => type;
        public int StartCapacity => startCapacity;
        public GameObject AssetReference => assetReference;
    }
}