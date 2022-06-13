using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private int _damage = 10;
        [SerializeField] private PooledObject _pooledObject = null;
        
        private LinkedList<DestructibleObject> _affectedObjects = null;

        private void OnEnable()
        {
            _affectedObjects = new LinkedList<DestructibleObject>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DestructibleObject>(out DestructibleObject destructible))
            {
                _affectedObjects.AddLast(destructible);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<DestructibleObject>(out DestructibleObject destructible))
            {
                _affectedObjects.Remove(destructible);
            }
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<DestructibleObject>(out DestructibleObject destructible)){

                if (_affectedObjects.Find(destructible) == null)
                {
                    _affectedObjects.AddLast(destructible);
                }

                Explode();
            }
        }

        private void Explode()
        {
            EffectsManager.SetupExplosion(PooledObjectType.GAMMAZOID_EXPLOSION, transform.position, Quaternion.identity);
            
            foreach (DestructibleObject destructibleObject in _affectedObjects)
            {
                destructibleObject.MakeDamage(_damage);
            }

            if (_pooledObject != null)
            {
                _pooledObject.ReturnToPool();
            }
        }
        
        private void OnDisable()
        {
            _affectedObjects.Clear();
        }
    }
}