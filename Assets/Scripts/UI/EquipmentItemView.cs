﻿using Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class EquipmentItemView : MonoBehaviour
    {
        [SerializeField] private Image _background = null;
        [SerializeField] private Image _icon = null;
        [SerializeField] private TextMeshProUGUI _header = null;
        [SerializeField] private TextMeshProUGUI _decription = null;

        private const string LockedHeader = "Locked";
        private const string LockedDescription = "It will open later.";
        
        public void SetData(EquipmentItemType type)
        {
            EquipmentItemState state = EquipmentUtils.GetState(type);

            DockMenu.EquipmentColors _equipmentColors = DockMenu.Instance.EquipmentListColors;
            EquipmentItemInfo info = default;
            
            switch (state)
            {
                case EquipmentItemState.LOCKED:

                    _header.text = LockedHeader;
                    _decription.text = LockedDescription;
                    _background.color = _equipmentColors.LockedColor;
                    break;
                
                case EquipmentItemState.AVAILABLE:

                    info = EquipmentUtils.GetInfo(type);
                    _header.text = info.Name;
                    _decription.text = info.Description;
                    _background.color = _equipmentColors.AvailableColor;
                    
                    break;

                case EquipmentItemState.EQUIPPED:

                    info = EquipmentUtils.GetInfo(type);
                    _header.text = info.Name;
                    _decription.text = info.Description;
                    _background.color = _equipmentColors.SelectedColor;
               
                    break;
                
            }
            
        }
    }
}