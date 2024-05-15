using System;

namespace CodeBase.Core.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public PlayerCarData PlayerCarData;
        public CoinData CoinData;
        public BestTimeData BestTimeData;
        public AudioControlData AudioControlData;

        public PlayerProgress(PlayerCarData playerCarData,
            CoinData coinData, BestTimeData bestTimeData, 
            AudioControlData audioControlData)
        {
            PlayerCarData = playerCarData;
            CoinData = coinData;
            BestTimeData = bestTimeData;
            AudioControlData = audioControlData;
        }
    }
}