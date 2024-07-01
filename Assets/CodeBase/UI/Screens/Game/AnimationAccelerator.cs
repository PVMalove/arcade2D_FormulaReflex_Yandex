using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens.Game
{
    public class AnimationAccelerator : MonoBehaviour
    {
        [SerializeField] private Image targetRenderer;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private int frameRate;

        private int currentSpriteIndex;
        private float secondPerFrame;
        private Sequence sequence;

        private void OnEnable()
        {
            currentSpriteIndex = default;
            secondPerFrame = 1f / frameRate;
            PlayAnimation();
        }

         
        private void OnDisable()
        {
            sequence.Stop();
            Tween.StopAll(this);
        }

        private void PlayAnimation()
        {
            sequence = Sequence.Create(-1)
                .ChainDelay(secondPerFrame)
                .ChainCallback(() =>
                {
                    targetRenderer.sprite = sprites[currentSpriteIndex];
                    currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
                });
        }
    }
}