using Core.Utilities.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Toggles
{
    public class SwitchToggle : Toggle
    {
        [SerializeField] private RectTransform handleRect;
        [SerializeField] private RectTransform toggleOnGraphic;
        [SerializeField] private RectTransform toggleOffGraphic;
        [SerializeField] private float transitionDuration = 0.2f;

        private Vector2 _handleOnPosition;
        private Vector2 _handleOffPosition;

        protected override void OnEnable()
        {
            _handleOnPosition = handleRect.anchoredPosition;
            _handleOffPosition = _handleOnPosition * -1;

            onValueChanged.AddListener(Switch);

            base.OnEnable();
        }

        private void Switch(bool on)
        {
            toggleOnGraphic.gameObject.SetActive(on);
            toggleOffGraphic.gameObject.SetActive(!on);

            handleRect.DOAnchorPos(on ? _handleOnPosition.Abs() : _handleOffPosition, transitionDuration)
                .SetUpdate(true).SetEase(Ease.InOutBack);
        }

        protected override void OnDisable()
        {
            onValueChanged.RemoveListener(Switch);

            base.OnDisable();
        }
    }
}