using System;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class DestructibleObject : MonoBehaviour
    {
        [SerializeField] private int _health = 100;

        public double Health => _health;

        public Func<Tween> OnDeath = null;

        public void MakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                if (OnDeath != null)
                {
                    OnDeath().OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                    });
                }
                else gameObject.SetActive(false);
            }

        }
    }
}