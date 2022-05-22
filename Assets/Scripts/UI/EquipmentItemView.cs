using System;
using Entities;
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
        [SerializeField] private Button _button = null;
        
        private const string LockedHeader = "Locked";
        private const string LockedDescription = "It will open later.";

        private EquipmentItemType _type = EquipmentItemType.NONE;
        private EquipmentItemState _state = EquipmentItemState.LOCKED;
        
        private bool _dataInstalled = false;

        public EquipmentItemType Type => _type;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (_dataInstalled)
            {
                switch (_state)
                {
                    case EquipmentItemState.LOCKED:
                        
                        break;

                    case EquipmentItemState.AVAILABLE:

                        _state = EquipmentItemState.EQUIPPED;
                        _background.color = DockMenu.Instance.EquipmentListColors.SelectedColor;
                        EquipmentUtils.Equip(_type);
                        
                        break;
                    
                    case EquipmentItemState.EQUIPPED: break;
                    
                }
                
                
            }

        }

        public void SetState(EquipmentItemState state)
        {
            _state = state;
            Update();
        }

        public void Update()
        {
            if (_dataInstalled)
            {
                DockMenu.EquipmentColors _equipmentColors = DockMenu.Instance.EquipmentListColors;
                
                switch (_state)
                {
                    case EquipmentItemState.LOCKED:
                        
                        _background.color = _equipmentColors.LockedColor;
                        break;
                
                    case EquipmentItemState.AVAILABLE:
                        
                        _background.color = _equipmentColors.AvailableColor;
                    
                        break;

                    case EquipmentItemState.EQUIPPED:
                        
                        _background.color = _equipmentColors.SelectedColor;
               
                        break;
                }
                
            }
            
        }

        public EquipmentItemView SetData(EquipmentItemType type, EquipmentItemState state, EquipmentItemInfo info)
        {
            _type = type;
            _state = state;
            DockMenu.EquipmentColors _equipmentColors = DockMenu.Instance.EquipmentListColors;

            _icon.sprite = info.Icon;
            
            switch (_state)
            {
                case EquipmentItemState.LOCKED:

                    _header.text = LockedHeader;
                    _decription.text = LockedDescription;
                    _background.color = _equipmentColors.LockedColor;
                    break;
                
                case EquipmentItemState.AVAILABLE:
                    
                    _header.text = info.Name;
                    _decription.text = info.Description;
                    _background.color = _equipmentColors.AvailableColor;
                    
                    break;

                case EquipmentItemState.EQUIPPED:
                    
                    _header.text = info.Name;
                    _decription.text = info.Description;
                    _background.color = _equipmentColors.SelectedColor;
               
                    break;
                
            }

            _dataInstalled = true;
            
            return this;
        }
    }
}