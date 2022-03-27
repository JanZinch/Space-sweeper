using System;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class DestructibleObject : MonoBehaviour
    {
        public double Health { get; private set; } = 100.0f;

        public Func<Tween> OnDeath = null;

        public void MakeDamage(double damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                if (OnDeath != null)
                {
                    OnDeath().OnComplete(() =>
                    {
                        Destroy(this);
                    });
                }
                else Destroy(this);
            }

        }
    }
}