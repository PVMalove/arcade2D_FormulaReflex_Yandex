using System;
using System.Collections.Generic;
using CodeBase.Core.StaticData.UI.Shop;

namespace CodeBase.Core.Data
{
    [Serializable]
    public struct PlayerCarData
    {
        public List<CarViewType> ItemsCar;
        public CarViewType SelectedCarType;
        public PlayerCarData(List<CarViewType> itemsCar, CarViewType selectedCarType)
        {
            ItemsCar = itemsCar;
            SelectedCarType = selectedCarType;
        } 
    }
}