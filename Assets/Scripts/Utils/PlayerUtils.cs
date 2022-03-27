using System.Collections.Generic;
using Entities;

namespace Utils
{
    public static class PlayerUtils
    {
        private static Dictionary<EquipmentType, Equipment> _weapons = null;
        public static Equipment CurrentWeapon = null;


        public static void Initialize()
        {
            _weapons = new Dictionary<EquipmentType, Equipment>()
            {
                { EquipmentType.MACHINE_GUN, new Equipment(EquipmentType.MACHINE_GUN, 0.0, 10.0, ShopItemState.EQUIPPED) },
                { EquipmentType.FIREBALL, new Equipment(EquipmentType.FIREBALL, 0.0, 10.0, ShopItemState.BLOCKED) },
                { EquipmentType.ROCKETS, new Equipment(EquipmentType.ROCKETS, 0.0, 10.0, ShopItemState.BLOCKED) },
                { EquipmentType.LASER, new Equipment(EquipmentType.LASER, 0.0, 10.0, ShopItemState.BLOCKED) }
                
            };

        }


    }
}