using System;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class DestructibleObject : MonoBehaviour
    {
        [SerializeField] private int _health = 100;
        [SerializeField] private PooledObject _pooledObject = null;
        
        public double Health => _health;

        public event Func<Tween> OnDeath = null;
        
        
        public void MakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                WeaponNavigationScreen.Instance.FreeIfNeed(transform);
                
                if (OnDeath != null)
                {
                    OnDeath().OnComplete(() =>
                    {
                        _pooledObject.ReturnToPool();
                    });
                }
                else _pooledObject.ReturnToPool();
            }

        }
    }
}