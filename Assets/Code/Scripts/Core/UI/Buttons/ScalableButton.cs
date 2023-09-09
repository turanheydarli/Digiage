using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Buttons
{
    public class ScalableButton : Button
    {
        private Tween _scaleTween;

        [SerializeField] public float strength = 0.9f;
        [SerializeField] public float duration = 0.1f;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            if (_scaleTween != null)
            {
                if (_scaleTween.active && _scaleTween.IsPlaying())
                {
                    transform.DOScale(1, 0);
                    _scaleTween.Kill();
                }
            }

            switch (state)
            {
                case SelectionState.Pressed:
                    _scaleTween = transform.DOScale(strength, duration).SetUpdate(true);
                    break;

                default:
                    _scaleTween = transform.DOScale(1, 0).SetUpdate(true).SetEase(Ease.InOutBack);
                    break;
            }
        }
    }
}