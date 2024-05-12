using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace CodeBase.Core.Services.Randomizer
{
    public class RandomService : IRandomService
    {
        private readonly HashSet<int> _hashNumbers = new();
        private readonly int _duplicateChance = 50;

        public int Next(int minValue, int maxValue)
        {
            int randomNumber;
            int duplicateCheck = Random.Range(0, 100);

            do
            {
                randomNumber = Random.Range(minValue, maxValue);
            } 
            while (_hashNumbers.Contains(randomNumber) && duplicateCheck < _duplicateChance);
            _hashNumbers.Add(randomNumber);

            if (_hashNumbers.Count == maxValue)
                _hashNumbers.Clear();

            return randomNumber;
        }
    }
}