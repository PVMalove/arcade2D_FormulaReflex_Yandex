using System;
using System.Collections.Generic;
using CodeBase.Core.StaticData.UI.Shop;

namespace CodeBase.Core.Data
{
    [Serializable]
    public struct PlayerCarData
    {
        public List<CarType> OwnedItemsCar;
        public CarType SelectedCarType;
        public PlayerCarData(List<CarType> ownedItemsCar, CarType selectedCarType)
        {
            OwnedItemsCar = ownedItemsCar;
            SelectedCarType = selectedCarType;
        } 
    }
}