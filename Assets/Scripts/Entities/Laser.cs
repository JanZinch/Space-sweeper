using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

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
        private Vector3 _defaultMaxDistance = default;
        private PooledObject _microexplosions = null;
        private float _distanceToTarget = default;
        private float _maxDistance = default;
        
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
            Vector3 cachedPosition = _lineRenderer.GetPosition(1);
            _defaultMaxDistance = new Vector3(cachedPosition.x, cachedPosition.y, _direction.z * Mathf.Abs(cachedPosition.z));
            _lineRenderer.SetPosition(1, _defaultMaxDistance);
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

        private void Awake()
        {
            _maxDistance = _sphereCastPercents * Vector3.Distance(_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1));
            _defaultMaxDistance = _lineRenderer.GetPosition(1);
        }

        private void Update()
        {
            RaycastHit raycastHit;
            float distance = (_distanceToTarget != default)? _distanceToTarget : _maxDistance;
            
            if (Physics.SphereCast(_sourcePoint.position, _sphereCastRadius, _direction, out raycastHit, distance))
            {
                Debug.Log("Catched: " + raycastHit.transform.gameObject.name);

                DestructibleObject cachedObject = FindDestructibleObject(raycastHit);

                if (cachedObject != null)
                {
                    Debug.Log("Damage");
                    cachedObject.MakeDamage(_damage);
                }

                if (_distanceToTarget == default)
                {
                    _microexplosions =  PoolsManager.GetPooledObject(_explosionType, raycastHit.point, Quaternion.identity);
                    _microexplosions.GetLinkedComponent<ParticleSystem>().Play();
                    
                    Vector3 cachedPosition = _lineRenderer.GetPosition(1);
                    _distanceToTarget = Mathf.Abs(_sourcePoint.position.z - raycastHit.point.z + 5.0f);
                    _lineRenderer.SetPosition(1, new Vector3(cachedPosition.x, cachedPosition.y, _direction.z * _distanceToTarget));
                    
                }
                else 
                {
                    _microexplosions.transform.position = raycastHit.point;
                }
                
                
                
            }
            else
            {
                if (_distanceToTarget != default)
                {
                    _microexplosions.ReturnToPool();
                    _microexplosions = null;

                    _lineRenderer.SetPosition(1, _defaultMaxDistance);
                    _distanceToTarget = default;
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