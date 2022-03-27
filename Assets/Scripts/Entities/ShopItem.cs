namespace Entities
{
    public class ShopItem
    {
        public double Price { get; private set; }
        public ShopItemState State { get; private set; }

        public ShopItem(double price, ShopItemState state)
        {
            Price = price;
            State = state;
        }
    }
    
    public enum ShopItemState
    {
        BLOCKED = 0,
        AVAILABLE = 1,
        BOUGHT = 2,
        EQUIPPED = 3, 
    }

}