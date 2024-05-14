using System;
using UnityEngine.Serialization;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerProgress
    {
        [FormerlySerializedAs("playerCarViewData")] [FormerlySerializedAs("CarViewData")] public PlayerCarData playerCarData;
        public CoinData CoinData;
        public BestTimeData BestTimeData;
        public AudioControlData AudioControlData;

        public PlayerProgress(PlayerCarData playerCarData,
            CoinData coinData, BestTimeData bestTimeData, 
            AudioControlData audioControlData)
        {
            this.playerCarData = playerCarData;
            CoinData = coinData;
            BestTimeData = bestTimeData;
            AudioControlData = audioControlData;
        }
    }
}