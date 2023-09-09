using System.Collections;
using Code.Scripts.Models;
using Core.Utilities.Singletons;
using UnityEngine;

namespace Code.Scripts.ColorManipulators
{
    public class SkyManipulator : SingletonBase<SkyManipulator>
    {
        [SerializeField] private Material skyboxMaterial;
        [SerializeField] public float transitionDuration = 2.0f;


        private static readonly int TopColor = Shader.PropertyToID("_TopColor");
        private static readonly int BottomColor = Shader.PropertyToID("_BottomColor");

        public void ChangeSkybox(ColorModel color)
        {
            if (skyboxMaterial && skyboxMaterial.HasProperty(TopColor) && skyboxMaterial.HasProperty(BottomColor))
            {
                StartCoroutine(ChangeColorsSmoothly(color.topColor, color.bottomColor));
            }
            else
            {
                Debug.LogWarning("Material or shader properties not found.");
            }
        }

        private IEnumerator ChangeColorsSmoothly(Color targetTopColor, Color targetBottomColor)
        {
            float elapsedTime = 0;

            Color initialTopColor = skyboxMaterial.GetColor(TopColor);
            Color initialBottomColor = skyboxMaterial.GetColor(BottomColor);

            while (elapsedTime < transitionDuration)
            {
                skyboxMaterial.SetColor(TopColor,
                    Color.Lerp(initialTopColor, targetTopColor, elapsedTime / transitionDuration));
                skyboxMaterial.SetColor(BottomColor,
                    Color.Lerp(initialBottomColor, targetBottomColor, elapsedTime / transitionDuration));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            skyboxMaterial.SetColor(TopColor, targetTopColor);
            skyboxMaterial.SetColor(BottomColor, targetBottomColor);
        }
    }
}