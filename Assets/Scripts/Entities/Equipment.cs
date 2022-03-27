namespace Entities
{
    public class Equipment : ShopItem
    {
        public EquipmentType Type { get; private set; }
        public double Power { get; private set; }

        public Equipment(EquipmentType type, double price, double power, ShopItemState state) : base(price, state)
        {
            Type = type;
            Power = power;
        }
    }

    public enum EquipmentType
    {
        NONE = 0,
        
        // weapon
        
        MACHINE_GUN = 1,
        FIREBALL = 2,
        ROCKETS = 3,
        LASER = 4
        
        // armory
        
        
        
    }
}