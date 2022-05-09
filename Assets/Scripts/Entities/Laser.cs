using System;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour
    {
        [SerializeField] private PooledObjectType _explosionType = PooledObjectType.FIREBALL_EXPLOSION;
        [SerializeField] private LineRenderer _lineRenderer = null;
        [SerializeField] private float _maxWidth = 1.0f;
        [SerializeField] private int _damage = 1;
        [SerializeField] private PooledObject _pooledObject = null;

        private Transform _sourcePoint = null;
        private Vector3 _direction = Vector3.forward;

        public void TurnOn()
        {
            _lineRenderer.startWidth = _maxWidth;
            _lineRenderer.endWidth = 0.5f * _maxWidth;
        }

        public Laser SetSourcePoint(Transform sourcePoint)
        {
            _sourcePoint = sourcePoint;
            return this;
        }

        public Laser SetDirection(Vector3 direction)
        {
            _direction = direction;
            return this;
        }

        private DestructibleObject FindDestructibleObject(RaycastHit raycastHit)
        {
            if (raycastHit.collider.TryGetComponent<DestructibleObject>(out DestructibleObject destructibleObject))
            {
                return destructibleObject;
            }
            else
            {
                return raycastHit.transform.GetComponentInParent<DestructibleObject>();
            }
        }


        private void Update()
        {
            RaycastHit raycastHit;
            
            if (Physics.SphereCast(_sourcePoint.position, 1.0f, _direction, out raycastHit, 100.0f))
            {
                Debug.Log("Catched: " + raycastHit.transform.gameObject.name);

                DestructibleObject cachedObject = FindDestructibleObject(raycastHit);

                if (cachedObject != null)
                {
                    Debug.Log("Damage");
                    cachedObject.MakeDamage(_damage);
                }

                if (!raycastHit.collider.TryGetComponent<WeaponNavigationScreen>(out WeaponNavigationScreen navigationScreen))
                {
                    EffectsManager.SetupExplosion(_explosionType, raycastHit.point, Quaternion.identity);
                }

                
            }
        }
        

        /*public void OnCollision(DestructibleObject other)
        {
            Debug.Log("SPAM");
            other.MakeDamage(_damage);
        }


       


        /*private void OnCollisionStay(Collision collision)
        {
            Debug.Log("Catched: " + collision.gameObject.name);
            EffectsManager.SetupExplosion(_explosionType, transform.position, Quaternion.identity);
        }*/
        
        /*private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<WeaponNavigationScreen>(out WeaponNavigationScreen weaponNavigationScreen))
            {
                Debug.Log("Catched: " + other.gameObject.name);
                EffectsManager.SetupExplosion(_explosionType, transform.position, Quaternion.identity);
                
            }


            
        }*/


        public void TurnOff()
        {
            _lineRenderer.startWidth = _lineRenderer.endWidth = 0.0f;

            if (_pooledObject != null) _pooledObject.ReturnToPool();
        }

        

    }
}