using System;

namespace CodeBase.Core.Data
{
   [Serializable]
    public struct AudioControlData
    {
        public float AudioVolume;
        public bool EffectsOn;

        public AudioControlData(float audioVolume, bool effectsOn)
        {
            AudioVolume = audioVolume;
            EffectsOn = effectsOn;
        }
    }
}