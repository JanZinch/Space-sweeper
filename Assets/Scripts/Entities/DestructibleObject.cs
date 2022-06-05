using System;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class DestructibleObject : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private PooledObject _pooledObject = null;
        private int _health = default;
        
        public double Health => _health;
        public event Func<Tween> OnDeath = null;
        public event Action<int> OnTakeDamage = null;
        public event Action<int> OnHealthUpdate = null;
        public event Action OnRefresh = null;
        
        public bool IsAlive => _health > 0;

        private void Awake()
        {
            _health = _maxHealth;
        }

        public void SetMaxHealth(int health)
        {
            _health = _maxHealth = health;
        }

        private void Refresh()
        {
            _health = _maxHealth;
            OnRefresh?.Invoke();

            if (_pooledObject != null)
            {
                _pooledObject.ReturnToPool();
            }
        }

        public void MakeDamage(int damage)
        {
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);

            OnTakeDamage?.Invoke(damage);
            OnHealthUpdate?.Invoke(_health);
            
            Debug.Log(gameObject.name + " health: " + _health);
            
            if (_health <= 0)
            {
                WeaponNavigationScreen.PlayerNavigationScreen.FreeIfNeed(transform);

                if (OnDeath != null)
                {
                    OnDeath().OnComplete(Refresh);
                }
                else Refresh();
            }

        }

        /*private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Laser>(out Laser laser))
            {
                laser.OnCollision(this);
            }
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<Laser>(out Laser laser))
            {
                laser.OnCollision(this);
            }
        }*/
        
    }
}