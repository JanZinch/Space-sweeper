using System;
using System.Collections.Generic;
using Assets.Scripts.Setups;
using Entities;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class EquipmentItemSlot : MonoBehaviour
    {
        [SerializeField] private EquipmentSlotType _type = EquipmentSlotType.FIRST_WEAPON;
        [SerializeField] private Image _icon = null;
        [SerializeField] private Image _background = null;
        [SerializeField] private Button _button = null;
        
        private Color32 _defaultColor = new Color32(255, 255, 255, 255);

        private Color32 _selectedColor = new Color32(255, 255, 100, 255);
        
        private bool _isSelected = false;
        private const float SelectedScaling = 1.2f;

        private static List<EquipmentItemSlot> _slots = new List<EquipmentItemSlot>(4);
        
        private static event Action<EquipmentSlotType> OnSlotSelected = null;

        private static event Action<EquipmentItemType> OnItemEquipped = null;


        private void Awake()
        {
            _slots.Add(this);
        }


        public static void UpdateView(Sprite icon)
        {
            _slots.Find((slot) => slot._type == EquipmentUtils.CurrentSlot)._icon.sprite = icon;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            OnSlotSelected += OnSelected;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            OnSlotSelected -= OnSelected;
        }

        private void OnClick()
        {
            OnSlotSelected?.Invoke(_type);
        }

        private void OnSelected(EquipmentSlotType type)
        {
            if (_type == type)
            {
                Select();
            }
            else
            {
                Release();
            }

        }


        private void Select()
        {
            if (!_isSelected)
            {
                EquipmentUtils.CurrentSlot = _type;
                _background.color = _selectedColor;
                transform.localScale *= SelectedScaling;
                _isSelected = true;
            }
        }
        
        private void Release()
        {
            if (_isSelected)
            {
                _background.color = _defaultColor;
                transform.localScale = Vector3.one;
                _isSelected = false;
            }
        }

    }
}