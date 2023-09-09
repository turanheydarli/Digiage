using System;
using Code.Core.UI.Abstraction;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Utilities.Extensions
{
    public static class CanvasAnimationExtensions
    {
        public static void OpenWithAnimation(this CanvasBase canvas, float duration = 0.1f, Ease ease = Ease.Linear,
            Action onComplete = null, float delay = 0)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(true);
            sequence.AppendInterval(delay);
            sequence.Join(canvas.transform.DOScale(0, 0));
            sequence.AppendCallback(() => canvas.gameObject.SetActive(true));
            sequence.Append(canvas.transform.DOScale(1, duration).SetEase(ease));
            sequence.OnComplete(() => onComplete?.Invoke());
        }

        /// <summary>
        /// The popup GameObject must be on index 0.
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        public static void OpenPopupWithAnimation(this CanvasBase canvas, float duration = 0.06f,
            Ease ease = Ease.InOutBack,
            Action onComplete = null)
        {
            Transform popup = canvas.transform.GetChild(0);
            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(true);
            sequence.SetEase(ease);
            sequence.Join(popup.DOScale(0, 0));
            sequence.AppendCallback(() => canvas.gameObject.SetActive(true));
            sequence.Append(popup.DOScale(1, duration));
            sequence.OnComplete(() => onComplete?.Invoke());
        }

        public static void CloseWithAnimation(this CanvasBase canvas, float duration = 0.1f, Ease ease = Ease.Linear,
            Action onComplete = null, float delay = 0)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(delay);
            sequence.Append(canvas.transform.DOScale(0, duration).SetEase(ease));
            sequence.AppendCallback(() => canvas.gameObject.SetActive(false));
            sequence.Append(canvas.transform.DOScale(1, 0));
            sequence.OnComplete(() => onComplete?.Invoke());
        }


        /// <summary>
        /// The popup GameObject must be on index 0.
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        public static void ClosePopupWithAnimation(this CanvasBase canvas, float duration = 0.06f,
            Action onComplete = null)
        {
            Transform popup = canvas.transform.GetChild(0);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(popup.DOScale(0, duration));
            sequence.AppendCallback(() => canvas.gameObject.SetActive(false));

            sequence.OnComplete(() => onComplete?.Invoke());
        }

        public static void SmoothFillAnimation(this TMP_Text text, int newValue, float duration = 0.1f,
            string prefix = "", string suffix = "")
        {
            DOTween.To(() =>
                {
                    if (prefix != "")
                    {
                        text.text = text.text.Replace(prefix, "");
                    }

                    if (suffix != "")
                    {
                        text.text = text.text.Replace(suffix, "");
                    }

                    return int.Parse(text.text);
                }, setter => text.text = $"{prefix}{setter}",
                newValue, duration);
        }

        public static void SmoothFillSliderAnimation(this Slider slider, float newValue, float duration = 0.1f)
        {
            DOTween.To(() => slider.value, setter => slider.value = setter, newValue, duration);
        }
    }
}