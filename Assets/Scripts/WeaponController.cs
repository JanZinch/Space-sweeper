using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public bool _isForPlayer = false;
    [SerializeField] private List<Weapon> _attachedWeapons = null;
    
    private const string Fire1 = "Fire1";
    private const string Fire2 = "Fire2";
    
    private const int FirstWeapon = 0;
    private const int SecondWeapon = 1;
    
    private void Update()
    {
        if (_isForPlayer)
        {
            if (Input.GetButtonDown(Fire1))
            {
                _attachedWeapons[FirstWeapon].FireIfPossible();
            }
        
            if (PlayerUtils.SecondWeaponIsAvailable && Input.GetButtonDown(Fire2))
            {
                _attachedWeapons[SecondWeapon].FireIfPossible();
            }
        }

        
    }
}

