﻿using UnityEngine;
using UnityEngine.UI;

namespace Alexplay.OilRush.Library.View
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class CrossToggleButton : ToggleButton
    {
        [SerializeField] private GameObject _crossObject;

        protected override void UpdateState()
        {
            _crossObject.SetActive(!IsChecked);
        }
    }
}