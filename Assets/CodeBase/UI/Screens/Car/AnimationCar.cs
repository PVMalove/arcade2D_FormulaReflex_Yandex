using System;
using PrimeTween;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.UI.Screens.Car
{
    public class AnimationCar : MonoBehaviour
    {
        [SerializeField] private RectTransform targetCar;
        [SerializeField] private RectTransform targetWheelFront;
        [SerializeField] private RectTransform targetWheelRear;
        
        [SerializeField] TweenSettings tweenSettings_TargetCar = new TweenSettings(duration:0.8f, Ease.InQuart);
        
        private Sequence sequence;

        [ContextMenu("Play_Animation")]
        public void PlayAnimation()
        {
            if (!sequence.isAlive)
            {
                sequence = Sequence.Create()
                    .Group(Tween.EulerAngles(targetWheelRear.transform, startValue: Vector3.zero,
                        endValue: new Vector3(0, 0, 3500),
                        duration: 0.75f))
                    .Group(Tween.EulerAngles(targetWheelFront.transform, startValue: Vector3.zero,
                        endValue: new Vector3(0, 0, 1500),
                        duration: 1))
                    .Insert(atTime: 0.1f, Tween.UIAnchoredPositionX(targetCar, endValue: -1500f, tweenSettings_TargetCar));
            }
        }

        [ContextMenu("Reset_Animation")]
        public void ResetAnimation()
        {
            Tween.StopAll(this);
            Sequence.Create()
                .Group(Tween.UIAnchoredPositionX(targetCar, endValue: 0f, duration: 0.3f, Ease.InQuart));
            targetWheelFront.localEulerAngles = Vector3.zero;
            targetWheelRear.localEulerAngles = Vector3.zero;
        }
    }
}