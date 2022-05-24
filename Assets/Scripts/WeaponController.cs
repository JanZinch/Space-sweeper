using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities;
using UnityEngine;
using Utils;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public bool _isForPlayer = false;
    [SerializeField] private WeaponNavigationScreen _navigationScreen = null;
    [SerializeField] private List<Weapon> _attachedWeapons = null;
    
    private const string Fire1 = "Fire1";
    private const string Fire2 = "Fire2";
    
    private const int FirstWeapon = 0;
    private const int SecondWeapon = 1;

    public WeaponNavigationScreen NavigationScreen => _navigationScreen;
    
    private void OnNewTarget(Transform target)
    {
        INavigational cachedWeapon = null;
        
        foreach (Weapon weapon in _attachedWeapons)
        {
            cachedWeapon = weapon as INavigational;

            if (cachedWeapon != null)
            {
                cachedWeapon.SetTarget(target);
            }
        }
    }

    private void Awake()
    {
        if (_isForPlayer)
        {
            EquipWeapons();
            _navigationScreen.SetForPlayer();
        }
    }

    private void OnEnable()
    {
        _navigationScreen.OnNewTarget += OnNewTarget;
    }

    private void EquipWeapons()
    {
        _attachedWeapons[0] = FindWeapon(PlayerUtils.FirstWeapon);
        _attachedWeapons[1] = FindWeapon(PlayerUtils.SecondWeapon);
    }

    private Weapon FindWeapon(EquipmentItemType weaponType)
    {
        switch (weaponType)
        {
            case EquipmentItemType.MACHINE_GUN: return GetComponent<MachineGun>();
            case EquipmentItemType.FIREBALL_GENERATOR: return GetComponent<FireballGenerator>();
            case EquipmentItemType.ROCKET_LAUNCHER: return GetComponent<RocketLauncher>();
            case EquipmentItemType.LASER_EMITTER: return GetComponent<LaserEmitter>();
            
        }
        
        return GetComponent<MachineGun>();
    }


    private void Update()
    {
        if (_isForPlayer)
        {
            if (_attachedWeapons[FirstWeapon].GetPlayerInput(Fire1))
            {
                _attachedWeapons[FirstWeapon].FireIfPossible();
            }
        
            if (PlayerUtils.SecondWeaponIsAvailable && _attachedWeapons[SecondWeapon].GetPlayerInput(Fire2))
            {
                _attachedWeapons[SecondWeapon].FireIfPossible();
            }
        }
    }

    public bool FireIfPossible(int weaponIndex)
    {
        return _attachedWeapons[weaponIndex].FireIfPossible();
    }

    public bool FireToPositionIfPossible(int weaponIndex, Vector3 position)
    {
        Weapon weapon = _attachedWeapons[weaponIndex];

        if (weapon != null)
        {
            weapon.SetCustomTargetPosition(position);
            return weapon.FireIfPossible();
        }

        return false;
    }


    private void OnDisable()
    {
        _navigationScreen.OnNewTarget -= OnNewTarget;
    }
}

