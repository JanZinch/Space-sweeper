using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Setups;
using CodeBase.ApplicationLibrary.Data;
using Entities;
using UnityEngine;

namespace Utils
{
    public static class EquipmentUtils
    {
        private static ReadOnlyDictionary<EquipmentItemType, EquipmentItem> _equipment = null;

        private static Exception _notInitializedException = null;
        
        public static bool IsInitialized { get; private set; } = false;


        public static void Initialize(EquipmentSetup setup)
        {
            _notInitializedException = new Exception("Equipment utils not initialized.");
            
            DataHelper _cachedDataHelper = Context.DataHelper; 
            
            _equipment = new ReadOnlyDictionary<EquipmentItemType, EquipmentItem>(
                new Dictionary<EquipmentItemType, EquipmentItem>()
                {
                    {EquipmentItemType.MACHINE_GUN, CreateEquipmentItem(EquipmentItemType.MACHINE_GUN, _cachedDataHelper, setup)},
                    {EquipmentItemType.FIREBALL_GENERATOR, CreateEquipmentItem(EquipmentItemType.FIREBALL_GENERATOR, _cachedDataHelper, setup)},
                    {EquipmentItemType.ROCKET_LAUNCHER, CreateEquipmentItem(EquipmentItemType.ROCKET_LAUNCHER, _cachedDataHelper, setup)},
                    {EquipmentItemType.LASER_EMITTER, CreateEquipmentItem(EquipmentItemType.LASER_EMITTER, _cachedDataHelper, setup)},
                
                    {EquipmentItemType.THICKENED_SHEATHING, CreateEquipmentItem(EquipmentItemType.THICKENED_SHEATHING, _cachedDataHelper, setup)},
                    {EquipmentItemType.ABLATIVE_COVERAGE, CreateEquipmentItem(EquipmentItemType.ABLATIVE_COVERAGE, _cachedDataHelper, setup)},
                    {EquipmentItemType.PROTECTOR_PROBES, CreateEquipmentItem(EquipmentItemType.PROTECTOR_PROBES, _cachedDataHelper, setup)},
                });
            
            IsInitialized = true;
        }

        private static void CheckInitialization()
        {
            if (!IsInitialized)
            {
                Debug.LogException(_notInitializedException);
            }
        }

        private static EquipmentItem CreateEquipmentItem(EquipmentItemType type, DataHelper dataHelper, EquipmentSetup setup)
        {
            return new EquipmentItem(type, 
                (EquipmentItemState) dataHelper.GetLong(EquipmentItem.GetDatabaseKey(type)), setup.GetInfo(type));
        }

        public static EquipmentItemState GetState(EquipmentItemType equipmentItemType)
        {
            CheckInitialization();
            return _equipment[equipmentItemType].State;
        }
        
        public static EquipmentItemInfo GetInfo(EquipmentItemType equipmentItemType)
        {
            CheckInitialization();

            EquipmentItem item = _equipment[equipmentItemType];
            return new EquipmentItemInfo(item.Type, item.Icon, item.Name, item.Description);
        }
        
        
        public static void Open(EquipmentItemType equipmentItemType)
        {
            CheckInitialization();
            _equipment[equipmentItemType].State = EquipmentItemState.AVAILABLE;
        }
        
        public static void Select(EquipmentItemType equipmentItemType)
        {
            CheckInitialization();
            _equipment[equipmentItemType].State = EquipmentItemState.EQUIPPED;
        }

    }
}