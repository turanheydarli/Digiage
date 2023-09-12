using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Models;
using Core.Utilities.Singletons;
using UnityEngine;

namespace Code.Scripts.ColorManipulators
{
    public class ColorHolder : SingletonBase<ColorHolder>
    {
        [SerializeField] private List<ColorModel> colors;

        private int _currentColorIndex = 1;

        public List<ColorModel> Colors => colors;

        public ColorModel GetInitialColor()
        {
            return colors.FirstOrDefault();
        }

        public ColorModel GetObstacleColor()
        {
            return colors.FirstOrDefault(c => c.colorId == SkyManipulator.Instance.CurrentColor.colorId);
        }

        public void AddColor(ColorModel color)
        {
            colors.Add(color);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SkyManipulator.Instance.ChangeSkybox(colors[_currentColorIndex]);

                _currentColorIndex++;

                if (_currentColorIndex >= colors.Count)
                {
                    _currentColorIndex = 0;
                }
            }
        }
    }
}