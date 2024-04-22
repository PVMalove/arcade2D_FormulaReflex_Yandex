using System;

namespace CodeBase.Core.Data
{
    [Serializable]
    public struct BestTimeData
    {
        public float Value;

        public BestTimeData(float Value)
        {
            this.Value = Value;
        }
    }
}