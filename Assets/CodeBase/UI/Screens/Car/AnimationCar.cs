using System;
using System.Globalization;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;


namespace CodeBase.UI.Screens.Car
{
    public class AnimationCar : MonoBehaviour
    {
        [SerializeField] private RectTransform targetCar;
        [SerializeField] private RectTransform targetWheelFront;
        [SerializeField] private RectTransform targetWheelRear;
        
        [SerializeField] private RectTransform targetCoins;
        [SerializeField] private RectTransform targetTextCoins;
        
        [SerializeField] private Text coinsAmountText;
        
        private Sequence sequence;
        private float coinValue;
        
        public void SetAmountCoins(float amount) => 
            coinValue = amount;

        [ContextMenu("Play_Animation")]
        public void PlayAnimation(string type)
        {
            if (sequence.isAlive) return;
            switch (type)
            {
                case "LostGame":
                    PlayLostGameAnimation();
                    break;
                case "EndGame":
                    PlayEndGameAnimation();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        [ContextMenu("Reset_Animation")]
        public void ResetAnimation()
        {
            Tween.StopAll(this);
            Sequence.Create()
                .Group(Tween.UIAnchoredPositionX(targetCar, endValue: 0f, duration: 0.25f, Ease.InQuart));
            targetWheelFront.localEulerAngles = Vector3.zero;
            targetWheelRear.localEulerAngles = Vector3.zero;
            targetCoins.gameObject.SetActive(false);
        }

        private void PlayLostGameAnimation()
        {
            ResetCarPosition();
            sequence = Sequence.Create()
                .Group(Tween.EulerAngles(targetWheelRear.transform, startValue: Vector3.zero,
                    endValue: new Vector3(0, 0, 1500),
                    duration: 0.75f))
                .Group(Tween.EulerAngles(targetWheelFront.transform, startValue: Vector3.zero,
                    endValue: new Vector3(0, 0, 500),
                    duration: 1))
                .Insert(atTime: 0.05f, Tween.UIAnchoredPositionX(targetCar, endValue: -500f, duration:0.6f, Ease.InQuart));
        }

        private void PlayEndGameAnimation()
        {
            ResetCarPosition();
            sequence = Sequence.Create()
                .Group(Tween.EulerAngles(targetWheelRear.transform, startValue: Vector3.zero,
                    endValue: new Vector3(0, 0, 3500),
                    duration: 0.75f))
                .Group(Tween.EulerAngles(targetWheelFront.transform, startValue: Vector3.zero,
                    endValue: new Vector3(0, 0, 1500),
                    duration: 1))
                .Insert(atTime: 0.1f, Tween.UIAnchoredPositionX(targetCar, endValue: -1500f, duration:0.8f, Ease.InQuart))
                .OnComplete(AddCoinsAnimation);
        }

        private void AddCoinsAnimation()
        {
            if (coinValue <= 0) return;
            targetCoins.gameObject.SetActive(true);
            sequence = Sequence.Create()
                .Group(Tween.Scale(targetCoins, startValue: 0.9f,
                    endValue: 1.2f,
                    duration: 0.25f,
                    Ease.InOutQuad))
                .Group(Tween.Custom(coinsAmountText, 0, coinValue, 1, UpdateCoinsText))
                .Chain(Tween.Scale(targetCoins, startValue: 1.2f, 
                    endValue: 1f, 
                    duration: 0.25f, 
                    Ease.InOutQuad));
        }
        
        private void UpdateCoinsText(Text target, float newValue) => 
            target.text = Mathf.Floor(newValue).ToString(CultureInfo.InvariantCulture);

        private void ResetCarPosition()
        {
            Tween.StopAll(this);
            targetCoins.gameObject.SetActive(false);
            targetCar.anchoredPosition = new Vector2(0, targetCar.anchoredPosition.y);
            targetWheelFront.localEulerAngles = Vector3.zero;
            targetWheelRear.localEulerAngles = Vector3.zero;
        }
    }
}