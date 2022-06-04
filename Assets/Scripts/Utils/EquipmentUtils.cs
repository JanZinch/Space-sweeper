using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Setups;
using CodeBase.ApplicationLibrary.Common;
using CodeBase.ApplicationLibrary.Data;
using Entities;
using UI;
using UnityEngine;

namespace Utils
{
    public static class EquipmentUtils
    {
        private static ReadOnlyDictionary<EquipmentItemType, EquipmentItem> _allEquipment = null;
        private static ReadOnlyDictionary<EquipmentItemType, EquipmentItem> _weapon = null;
        private static ReadOnlyDictionary<EquipmentItemType, EquipmentItem> _protection = null;
        
        private static Exception _notInitializedException = null;
        
        public static bool IsInitialized { get; private set; } = false;

        
        
        public struct SelectedEquipment
        {
            public EquipmentItem FirstWeapon;
            public EquipmentItem SecondWeapon;
            public EquipmentItem Protection;
        }
        

        public static void Initialize(EquipmentSetup setup)
        {
            _notInitializedException = new Exception("Equipment utils not initialized.");
            
            DataHelper _cachedDataHelper = Context.DataHelper; 
            
            _weapon = new ReadOnlyDictionary<EquipmentItemType, EquipmentItem>(
                new Dictionary<EquipmentItemType, EquipmentItem>()
                {
                    {EquipmentItemType.MACHINE_GUN, CreateEquipmentItem(EquipmentItemType.MACHINE_GUN, _cachedDataHelper, setup)},
                    {EquipmentItemType.FIREBALL_GENERATOR, CreateEquipmentItem(EquipmentItemType.FIREBALL_GENERATOR, _cachedDataHelper, setup)},
                    {EquipmentItemType.ROCKET_LAUNCHER, CreateEquipmentItem(EquipmentItemType.ROCKET_LAUNCHER, _cachedDataHelper, setup)},
                    {EquipmentItemType.LASER_EMITTER, CreateEquipmentItem(EquipmentItemType.LASER_EMITTER, _cachedDataHelper, setup)},
                });
            
            _protection = new ReadOnlyDictionary<EquipmentItemType, EquipmentItem>(
                new Dictionary<EquipmentItemType, EquipmentItem>()
                {
                    {EquipmentItemType.THICKENED_SHEATHING, CreateEquipmentItem(EquipmentItemType.THICKENED_SHEATHING, _cachedDataHelper, setup)},
                    {EquipmentItemType.ABLATIVE_COVERAGE, CreateEquipmentItem(EquipmentItemType.ABLATIVE_COVERAGE, _cachedDataHelper, setup)},
                    {EquipmentItemType.PROTECTOR_PROBES, CreateEquipmentItem(EquipmentItemType.PROTECTOR_PROBES, _cachedDataHelper, setup)},
                });

            Dictionary<EquipmentItemType, EquipmentItem> tempCollection = new Dictionary<EquipmentItemType, EquipmentItem>(_weapon.Count + _protection.Count);
            tempCollection.AddRange(_weapon);
            tempCollection.AddRange(_protection);

            _allEquipment = new ReadOnlyDictionary<EquipmentItemType, EquipmentItem>(tempCollection);

            OpenBaseItemsIfNeed();

            IsInitialized = true;
        }

        private static void OpenBaseItemsIfNeed()
        {
            EquipmentItem baseItem = _allEquipment[EquipmentItemType.MACHINE_GUN];
            
            if (baseItem.State == EquipmentItemState.LOCKED)
            {
                baseItem.State = EquipmentItemState.EQUIPPED;
            }
            
            if (_allEquipment[EquipmentItemType.PROTECTOR_PROBES].State == EquipmentItemState.AVAILABLE)
            {
                _allEquipment[EquipmentItemType.PROTECTOR_PROBES].State = EquipmentItemState.LOCKED;
            }
            
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
            /*return new EquipmentItem(type, 
                (EquipmentItemState) dataHelper.GetLong(EquipmentItem.GetDatabaseKey(type)), setup.GetInfo(type));*/
            
            return new EquipmentItem(type, 
                EquipmentItemState.AVAILABLE, setup.GetInfo(type));
        }

        

        public static List<EquipmentItemView> InitializeItemViewList(EquipmentItemView original, in RectTransform listView)
        {
            CheckInitialization();
            
            List<EquipmentItemView> _itemViews = new List<EquipmentItemView>(_allEquipment.Count);

            foreach (var item in _allEquipment)
            {
                _itemViews.Add(GameObject.Instantiate(original, listView).SetData(item.Value.Type, item.Value.State, item.Value.Info));
            }

            return _itemViews;
        }

        public static void Open(EquipmentItemType equipmentItemType)
        {
            CheckInitialization();
            _allEquipment[equipmentItemType].State = EquipmentItemState.AVAILABLE;
        }
        
        public static bool TryEquip(EquipmentItemType equipmentItemType)
        {
            CheckInitialization();
            
            EquipmentItem item = _allEquipment[equipmentItemType];

            if (EquipmentItemSlot.SetItemInCurrentSlot(equipmentItemType, item.Icon))
            {
                item.State = EquipmentItemState.EQUIPPED;
                return true;
            }
            else return false;
        }

        public static void Unequip(EquipmentItemType equipmentItemType)
        {
            CheckInitialization();
            EquipmentItem item = _allEquipment[equipmentItemType];
            item.State = EquipmentItemState.AVAILABLE;
            
            Messenger.Broadcast(MessengerKeys.ON_ITEM_UNEQUIPPED, 
                new Bundle().Set(BundleKeys.EQUIPMENT_ITEM_TYPE, equipmentItemType));
        }

        public static bool IsWeapon(EquipmentItemType equipmentItemType)
        {
            return _weapon.ContainsKey(equipmentItemType);
        }
        
        public static bool IsProtection(EquipmentItemType equipmentItemType)
        {
            return _protection.ContainsKey(equipmentItemType);
        }

        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if(target==null)
                throw new ArgumentNullException(nameof(target));
            if(source==null)
                throw new ArgumentNullException(nameof(source));
            foreach(var element in source)
                target.Add(element);
        }

    }
    
    public enum EquipmentSlotType
    {
        FIRST_WEAPON = 0,
        SECOND_WEAPON = 1,
        PROTECTION = 2,
    }
}