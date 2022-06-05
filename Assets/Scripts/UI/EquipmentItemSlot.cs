using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        private EquipmentItemType _equippedItem = EquipmentItemType.NONE;
        private bool _isSelected = false;
        private const float SelectedScaling = 1.2f;
        private bool _isLocked = false;
        
        private static List<EquipmentItemSlot> _slots = new List<EquipmentItemSlot>(4);
        public static EquipmentSlotType CurrentSlot { get; private set; } = EquipmentSlotType.FIRST_WEAPON;
        private static event Action<EquipmentSlotType> OnSlotSelected = null;
        
        private void Awake()
        {
            if (_type == EquipmentSlotType.FIRST_WEAPON)
            {
                Select();
            }

            _slots.Add(this);
        }
        
        public static bool TryUpdatePlayerUtils()
        {
            EquipmentItemType[] selectedEquipment = new EquipmentItemType[3];
            
            if (CheckSlotContent(EquipmentSlotType.FIRST_WEAPON, out selectedEquipment[0]) && 
                CheckSlotContent(EquipmentSlotType.SECOND_WEAPON, out selectedEquipment[1]) &&
                CheckSlotContent(EquipmentSlotType.PROTECTION, out selectedEquipment[2]))
            {
                PlayerUtils.SetEquipment(selectedEquipment[0], selectedEquipment[1], selectedEquipment[2]);
                return true;
            }
            else
            {
                //throw new Exception("Invalid equipment for player.");
                return false;
            }
        }

        private static bool CheckSlotContent(EquipmentSlotType slotType, out EquipmentItemType itemType)
        {
            EquipmentItemSlot slot = _slots.Find((slot) => slot._type == slotType);
            itemType = slot._equippedItem;
            
            if (itemType == EquipmentItemType.NONE)
            {
                return slot._isLocked;
            }
            else 
                return IsCompatible(slot, itemType, true);
        }

        private static bool IsCompatible(EquipmentItemSlot slot, EquipmentItemType item, bool onStartGameSession = false)
        {
            if (onStartGameSession || _slots.Find((slot) => slot._equippedItem == item) == null)
            {
                switch (slot._type)
                {
                    case EquipmentSlotType.FIRST_WEAPON:
                    case EquipmentSlotType.SECOND_WEAPON:
                        return EquipmentUtils.IsWeapon(item);
                    
                    case EquipmentSlotType.PROTECTION:
                        return EquipmentUtils.IsProtection(item);
                    
                    default:
                        throw new InvalidEnumArgumentException("slotType", (int) slot._type,
                            typeof(EquipmentItemType));
                }
            }
            else return false;
        }

        public static bool SetItemInCurrentSlot(EquipmentItemType item, Sprite icon)
        {
            EquipmentItemSlot slot = _slots.Find((slot) => slot._type == CurrentSlot);

            if (IsCompatible(slot, item))
            {
                if (slot._equippedItem != EquipmentItemType.NONE)
                {
                    EquipmentUtils.Unequip(slot._equippedItem);
                }

                slot._equippedItem = item;
                slot._icon.sprite = icon;

                return true;
            }
            else return false;
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
                CurrentSlot = _type;
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

        public static void Refresh()
        {
            CurrentSlot = EquipmentSlotType.FIRST_WEAPON;
        }
        
    }
}