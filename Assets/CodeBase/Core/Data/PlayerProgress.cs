using System;
using UnityEngine.Serialization;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public AudioControlData AudioControlData;
        public CoinData CoinData;
        
        public PlayerProgress(AudioControlData audioControlData,
            CoinData coinData)
        {
            AudioControlData = audioControlData;
            CoinData = coinData;
        }
    }
}