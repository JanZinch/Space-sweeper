using System;
using System.Collections.Generic;
using Entities;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Setups
{
    [CreateAssetMenu(fileName = "EquipmentSetup", menuName = "Application/EquipmentSetup", order = 0)]
    public class EquipmentSetup : ScriptableObject
    {
        [SerializeField] private List<EquipmentItemInfo> _equipment = null;

        private static EquipmentSetup _instance = null;
        
        public EquipmentItemInfo GetInfo(EquipmentItemType type)
        {
            return _equipment.Find((item) => item.Type == type);
        }
    }
}

[Serializable]
public struct EquipmentItemInfo
{
    public EquipmentItemType Type;
    public Sprite Icon;
    public string Name;
    public string Description;

    public EquipmentItemInfo(EquipmentItemType type, Sprite icon, string name, string description)
    {
        Type = type;
        Icon = icon;
        Name = name;
        Description = description;
    }
}