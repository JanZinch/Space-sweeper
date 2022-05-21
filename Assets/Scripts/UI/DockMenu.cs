using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class DockMenu : MonoBehaviour
    {
        [field: SerializeField] public EquipmentColors EquipmentListColors { get; private set; } = default;

        [SerializeField] private EquipmentItemView _equipmentViewOriginal = null;
        [SerializeField] private RectTransform _equipmentListView = null;
        private List<EquipmentItemView> _equipmentList = null;
        
        public static DockMenu Instance { get; private set; } = null;
        
        [Serializable]
        public struct EquipmentColors
        {
            [field: SerializeField] public Color LockedColor { get; private set; }
            [field: SerializeField] public Color AvailableColor { get; private set; }
            [field: SerializeField] public Color SelectedColor { get; private set; }
        }

        private void Awake()
        {
            Instance = this;
            
            _equipmentList = EquipmentUtils.InitializeItemViewList(_equipmentViewOriginal, in _equipmentListView);
            
        }
    }
}