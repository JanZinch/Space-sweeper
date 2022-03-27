namespace CodeBase.ApplicationLibrary.Common
{
    public enum MessengerKeys
    {
        SERVICE_INITIALIZED = -100,
        NONE = 0,
        
        ON_LOCATION_CLICKED = 1,
        RESIDUE_CHANGED = 2,
        LOCATION_ENTERED = 3,
        LOCATION_PUCHASED = 4,
        
        #region COUNTERS
        MONEY = 10,
        TICKETS = 11,
        RUBBIES = 12,
        
        TAP = 30,
        #endregion
        
        TEST = 777,
        
        ON_MONEY_CHANGED = 42,
        ON_TICKETS_CHANGED = 43,
        ON_RUBBIES_CHANGED = 44,
        
        REFRESH_HOUSE_VIEW = 100,
        REFRESH_HOUSE_SELECTOR = 101,
        REFRESH_PLAYER_VIEW = 102,
        CHANGE_HOUSE_SELECTOR_PLACE = 103,
        
        REFRESH_COUNTER_EXP = 120,
        REFRESH_COUNTER_MONEY = 121,
        REFRESH_COUNTER_PREM_MONEY = 122,
        REFRESH_EQUIPMENT_ELEMENTS = 123,
        
        CLOSE_ALL_HOUSE_SELECTOR = 200,
        
        REATTACK_HOUSE = 300,
        DECREASE_CURRENT_BUILDINGS_HP = 301,
        
        ON_X2_BUTTON_CLICK = 302,                   // нажатие на конпку х2 в правом нижнем углу экрана 
        
        
        ON_X2_BOOSTER_EXECUTED = 402,
        ON_X2_BOOSTER_FINISHED = 403,
        
        ON_RAVEN_BOOSTER_EXECUTED = 404,
        ON_RAVEN_BOOSTER_FINISHED = 405,
        
        ON_GOD_TOUCH_BOOSTER_EXECUTED = 406,
        ON_GOD_TOUCH_BOOSTER_FINISHED = 407,
        
        ON_DARKNESS_BOOSTER_EXECUTED = 408,
        ON_DARKNESS_BOOSTER_FINISHED = 409,
        
        ON_MONEY_BOOSTER_EXECUTED = 408,
        ON_MONEY_BOOSTER_FINISHED = 409,
        
    }

    public enum BundleKeys
    {
        NONE = 0,
        CHANGE_VALUE = 1,
        NEED_ANIMATE = 2,
        UPDATED_VALUE = 3,
        MULTIPLIER = 4,

    }
}