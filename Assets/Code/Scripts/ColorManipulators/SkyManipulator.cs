using System;
using System.Collections;
using Code.Scripts.Models;
using Core.Utilities.Singletons;
using UnityEngine;

namespace Code.Scripts.ColorManipulators
{
    public class SkyManipulator : SingletonBase<SkyManipulator>
    {
        public static event Action<ColorModel> OnSkyColorChange;
        public ColorModel CurrentColor => _colorModel;

        [SerializeField] private Material skyboxMaterial;
        [SerializeField] public float transitionDuration = 2.0f;

        private ColorModel _colorModel;

        private static readonly int TopColor = Shader.PropertyToID("_TopColor");
        private static readonly int BottomColor = Shader.PropertyToID("_BottomColor");

        private void Start()
        {
            _colorModel = ColorHolder.Instance.GetInitialColor();

            ChangeSkybox(_colorModel);
        }

        public void ChangeSkybox(ColorModel color)
        {
            if (skyboxMaterial && skyboxMaterial.HasProperty(TopColor) && skyboxMaterial.HasProperty(BottomColor))
            {
                StartCoroutine(ChangeColorsSmoothly(color));
            }
            else
            {
                Debug.LogWarning("Material or shader properties not found.");
            }
        }

        private IEnumerator ChangeColorsSmoothly(ColorModel colorModel)
        {
            float elapsedTime = 0;

            Color initialTopColor = skyboxMaterial.GetColor(TopColor);
            Color initialBottomColor = skyboxMaterial.GetColor(BottomColor);

            _colorModel = colorModel;

            OnSkyColorChange?.Invoke(_colorModel);

            while (elapsedTime < transitionDuration)
            {
                skyboxMaterial.SetColor(TopColor,
                    Color.Lerp(initialTopColor, colorModel.topColor, elapsedTime / transitionDuration));
                skyboxMaterial.SetColor(BottomColor,
                    Color.Lerp(initialBottomColor, colorModel.bottomColor, elapsedTime / transitionDuration));

                RenderSettings.fogColor =
                    Color.Lerp(initialBottomColor, colorModel.bottomColor, elapsedTime / transitionDuration);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            skyboxMaterial.SetColor(TopColor, colorModel.topColor);
            skyboxMaterial.SetColor(BottomColor, colorModel.bottomColor);
        }
    }
}