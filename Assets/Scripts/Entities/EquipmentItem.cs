using System.Collections.ObjectModel;
using Assets.Scripts.Setups;
using UnityEngine;

namespace Entities
{
    public class EquipmentItem
    {
        public EquipmentItemType Type { get; private set; }
        public EquipmentItemState State { get; set; }

        public string Name { get; private set; } = null;
        public string Description { get; private set; } = null;
        public Sprite Icon { get; private set; } = null;

        public EquipmentItemInfo Info => new EquipmentItemInfo(Type, Icon, Name, Description);

        private const string KeyPrefix = "equipment_";
        
        public EquipmentItem(EquipmentItemType type, EquipmentItemState state, EquipmentItemInfo info)
        {
            Type = type;
            State = state;

            Name = info.Name;
            Description = info.Description;
            Icon = info.Icon;
        }

        public static string GetDatabaseKey(EquipmentItemType type)
        {
            return KeyPrefix + type.ToString().ToLower();
        }
        
        
    }
    
    public enum EquipmentItemState
    {
        LOCKED = 0,
        AVAILABLE = 1,
        EQUIPPED = 2, 
    }
    
    public enum EquipmentItemType
    {
        NONE = 0,
        
        MACHINE_GUN = 1,
        FIREBALL_GENERATOR = 2, 
        ROCKET_LAUNCHER = 3,
        LASER_EMITTER = 4,
        
        THICKENED_SHEATHING = 5,
        ABLATIVE_COVERAGE = 6,
        PROTECTOR_PROBES = 7,
    }

}