using System;
using UnityEngine;

namespace Entities
{
    public class Enemy : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<WeaponNavigationScreen>(out WeaponNavigationScreen weaponNavigationScreen))
            {
                weaponNavigationScreen.OnTrigger(transform);
            }
        }

    }
}