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
        private Vector3 _defaultEndPosition = default;
        private float _distanceToTarget = -1.0f;
        private float _maxDistance = default;

        private const float DistanceOffset = 1.5f;
        private PooledObject _microexplosions = null;

        private void Awake()
        {
            _maxDistance = _sphereCastPercents * Vector3.Distance(_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1));
            _defaultEndPosition = _lineRenderer.GetPosition(1);
        }
        
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
            _defaultEndPosition = new Vector3(_defaultEndPosition.x, _defaultEndPosition.y, _direction.z * Mathf.Abs(_defaultEndPosition.z));
            _lineRenderer.SetPosition(1, _defaultEndPosition);
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

        private float GetRaycastDistance()
        {
            return (_distanceToTarget >= 0.0f) ? _distanceToTarget : _maxDistance;
        }

        private void Update()
        {
            RaycastHit raycastHit;

            if (Physics.SphereCast(_sourcePoint.position, _sphereCastRadius, _direction, out raycastHit, GetRaycastDistance()))
            {
                Debug.Log("Catched: " + raycastHit.transform.gameObject.name);

                DestructibleObject cachedObject = FindDestructibleObject(raycastHit);

                if (cachedObject != null)
                {
                    cachedObject.MakeDamage(_damage);
                }

                _distanceToTarget = Mathf.Abs(_sourcePoint.position.z - raycastHit.point.z);
                
                Vector3 cachedPosition = _lineRenderer.GetPosition(1);
                _lineRenderer.SetPosition(1, new Vector3(cachedPosition.x, cachedPosition.y, _direction.z * (_distanceToTarget + DistanceOffset)));

                if (_microexplosions == null)
                {
                    _microexplosions = PoolsManager.GetPooledObject(_explosionType, raycastHit.point, Quaternion.identity);
                    _microexplosions.GetLinkedComponent<ParticleSystem>().Play();
                }
                else
                {
                    _microexplosions.transform.position = raycastHit.point;
                }
            }
            else
            {
                _distanceToTarget = -1.0f;
                _lineRenderer.SetPosition(1, _defaultEndPosition);

                if (_microexplosions != null)
                {
                    _microexplosions.ReturnToPool();
                    _microexplosions = null;
                }
            }
        }
        
        public void TurnOff()
        {
            _lineRenderer.startWidth = _lineRenderer.endWidth = 0.0f;

            if (_pooledObject != null) _pooledObject.ReturnToPool();
        }
        
    }
}