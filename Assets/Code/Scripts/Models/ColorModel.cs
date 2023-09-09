using System;
using UnityEngine;

namespace Code.Scripts.Models
{
    [Serializable]
    public class ColorModel
    {
        public int colorId;
        public Color topColor;
        public Color bottomColor;
        public string colorName;
    }
}