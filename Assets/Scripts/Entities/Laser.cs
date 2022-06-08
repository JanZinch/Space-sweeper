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
        [SerializeField] private float _sphereCastRadius = 1.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float _sphereCastPercents = 1.0f; 
        [SerializeField] private PooledObject _pooledObject = null;
        
        private Transform _sourcePoint = null;
        private Vector3 _direction = Vector3.forward;

        private float _shootingDuration = -1.0f;
        
        public void TurnOn()
        {
            _lineRenderer.startWidth = _maxWidth;
            _lineRenderer.endWidth = 0.5f * _maxWidth;

            if (_shootingDuration >= 0.0f)
            {
                Invoke(nameof(TurnOn), _shootingDuration);
            }
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
            float maxDistance = _sphereCastPercents * Vector3.Distance(_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1));
            
            if (Physics.SphereCast(_sourcePoint.position, _sphereCastRadius, _direction, out raycastHit, maxDistance))
            {
                Debug.Log("Catched: " + raycastHit.transform.gameObject.name);

                DestructibleObject cachedObject = FindDestructibleObject(raycastHit);

                if (cachedObject != null)
                {
                    Debug.Log("Damage");
                    cachedObject.MakeDamage(_damage);
                }
                
                EffectsManager.SetupExplosion(_explosionType, raycastHit.point, Quaternion.identity);
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

        public void SetShootingDuration(float shootingDuration)
        {
            _shootingDuration = shootingDuration;
        }

        public void TurnOff()
        {
            _lineRenderer.startWidth = _lineRenderer.endWidth = 0.0f;

            if (_pooledObject != null) _pooledObject.ReturnToPool();
        }

        

    }
}