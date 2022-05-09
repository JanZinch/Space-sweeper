using System;
using UnityEngine;

namespace Entities
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected DestructibleObject _destructibleObject = null;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_destructibleObject.IsAlive && other.TryGetComponent<WeaponNavigationScreen>(out WeaponNavigationScreen weaponNavigationScreen))
            {
                weaponNavigationScreen.OnTrigger(transform);
            }
        }

    }
}