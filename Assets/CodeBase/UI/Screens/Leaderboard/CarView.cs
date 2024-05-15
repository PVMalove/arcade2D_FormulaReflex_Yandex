using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens.Leaderboard
{
    public class CarView : MonoBehaviour
    {
        [SerializeField] private Image imageCar;

        public void SetSprite(Sprite sprite)
        {
            imageCar.sprite = sprite;
        }
    }
}