using System;
using UnityEngine;

namespace UI
{
    public class DockMenu : MonoBehaviour
    {
        [field: SerializeField] public EquipmentColors EquipmentListColors { get; private set; } = default;

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
        }
    }
}