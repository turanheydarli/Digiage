using System;
using System.Collections.Generic;
using Code.Scripts.ColorManipulators;
using Code.Scripts.Models;
using DG.Tweening;
using UnityEngine;

public class ColorCanvas : MonoBehaviour
{
    [SerializeField] private List<ColorItem> colorItems;

    private void Start()
    {
        SkyManipulator.OnSkyColorChange += ChangeColorUI;
    }

    private void ChangeColorUI(ColorModel colorModel)
    {
        foreach (var item in colorItems)
        {
            if (item.colorId == colorModel.colorId)
            {
                item.transform.DOScale(1, 0.15f);
            }
            else
            {
                item.transform.DOScale(0.8f, 0.15f);
            }
        }
    }
}