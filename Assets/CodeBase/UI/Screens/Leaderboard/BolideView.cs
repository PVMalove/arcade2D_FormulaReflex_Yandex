using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CodeBase.UI.Screens.Leaderboard
{
    public class BolideView : MonoBehaviour
    {
        [SerializeField] private Image imageBolide;

        public void SetSprite(Sprite sprite)
        {
            imageBolide.sprite = sprite;
        }
    }
}