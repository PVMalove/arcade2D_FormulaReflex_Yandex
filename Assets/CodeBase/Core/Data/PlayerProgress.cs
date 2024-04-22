using System;
using UnityEngine.Serialization;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public AudioControlData AudioControlData;
        public CoinData CoinData;
        public BestTimeData BestTimeData;
        
        public PlayerProgress(AudioControlData audioControlData,
            CoinData coinData, BestTimeData bestTimeData)
        {
            AudioControlData = audioControlData;
            CoinData = coinData;
            BestTimeData = bestTimeData;
        }
    }
}