using PrimeTween;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class NotifyAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 startScale;
        [SerializeField] private Vector3 endScale;
        [SerializeField] private float duration;
        
        private Sequence sequence;

        private void OnEnable()
        {
            AnimateScale();
        }

        private void OnDisable()
        {
            sequence.Stop();
        }

        private void AnimateScale()
        {
            sequence = Sequence.Create(-1)
                .Chain(Tween.Scale(transform, startScale, endScale, duration, Ease.InOutQuad))
                .Chain(Tween.Scale(transform, endScale, startScale, duration, Ease.InOutQuad));
        }
    }
}