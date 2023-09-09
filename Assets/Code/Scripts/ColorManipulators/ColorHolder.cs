using System;
using System.Collections.Generic;
using Code.Scripts.Models;
using UnityEngine;

namespace Code.Scripts.ColorManipulators
{
    public class ColorHolder : MonoBehaviour
    {
        [SerializeField] private List<ColorModel> colors;

        public List<ColorModel> Colors => colors;

        public void AddColor(ColorModel color)
        {
            colors.Add(color);
        }

        private void Update()
        {
            if ((Input.GetKeyDown(KeyCode.A)))
            {
                SkyManipulator.Instance.ChangeSkybox(colors[0]);
            }
        }
    }
}