using System;

namespace CodeBase.Core.Data
{
   [Serializable]
    public struct AudioControlData
    {
        public float AudioVolume;
        public bool MusicOn;
        public bool EffectsOn;

        public AudioControlData(float audioVolume, bool musicOn, bool effectsOn)
        {
            AudioVolume = audioVolume;
            MusicOn = musicOn;
            EffectsOn = effectsOn;
        }
    }
}