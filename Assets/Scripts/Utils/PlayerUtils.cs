using System.Collections.Generic;
using Entities;
using UnityEditor;

namespace Utils
{
    public static class PlayerUtils
    {
        public static bool SecondWeaponIsAvailable = true;

        public static EquipmentItemType FirstWeapon { get; private set; } = EquipmentItemType.NONE;
        public static EquipmentItemType SecondWeapon { get; private set; } = EquipmentItemType.NONE;
        public static EquipmentItemType Protection { get; private set; } = EquipmentItemType.NONE;
        
        public static int MaxHealth { get; set; } = 0;

        private const int _defaultHealth = 100000;
        public static bool EquipmentInitialized { get; private set; } = false;

        public static void SetEquipment(EquipmentItemType firstWeapon, EquipmentItemType secondWeapon, EquipmentItemType protection)
        {
            FirstWeapon = firstWeapon;
            SecondWeapon = secondWeapon;
            Protection = protection;

            MaxHealth = (protection == EquipmentItemType.THICKENED_SHEATHING) ? 
                (int) (_defaultHealth * GameBalance.ThickenedSheathingMultiplier)
                : _defaultHealth;

            EquipmentInitialized = true;
        }
        
        
        

    }
}