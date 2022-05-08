using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private PooledObjectType _explosionType = PooledObjectType.FIREBALL_EXPLOSION;
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private float _speed = 100.0f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private ParticleSystem _trailParticles = null;
    [SerializeField] private Navigation _navigation = null;
    [SerializeField] private PooledObject _pooledObject = null;
    
    
    
    
    [Serializable]
    public class Navigation
    {
        [SerializeField] private float _lerpDuration = 1.0f; 
        private Transform _target = null;

        private Vector3 _startPosition = default;
        private float _currentLerpTime = 0.0f;

        public Transform Target => _target;
        
        public void Setup(Vector3 startPosition, Transform target)
        {
            _startPosition = startPosition;
            _target = target;
        }

        public Vector3 Update()
        {
            //_currentLerpTime = Mathf.Clamp(_currentLerpTime += Time.deltaTime, 0.0f, _lerpDuration);
            
            _currentLerpTime += Time.deltaTime;
            if (_currentLerpTime > _lerpDuration )
            {
                _currentLerpTime = _lerpDuration ;
            }
            
            float percentComplete = _currentLerpTime / _lerpDuration;

            return Vector3.Lerp(_startPosition, _target.position, percentComplete);
        }

    }


    public Projectile Setup(Vector3 direction)
    {
        Setup(new Vector3(direction.x * _speed, direction.y * _speed, direction.z * _speed), _damage);
        return this;
    }

    public Projectile AddNavigation(Transform target)
    {
        _navigation?.Setup(transform.position, target);
        return this;
    }

    private void Setup(Vector3 velocity, int damage)
    {
        _rigidbody.velocity = velocity;
        _damage = damage;
        
        if(_trailParticles) _trailParticles.Play();
    }

    private void FixedUpdate()
    {
        if (_navigation != null && _navigation.Target != null)
        {
            _rigidbody.MovePosition(_navigation.Update());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision with: " + other.gameObject.name);
        
        if (other.gameObject.TryGetComponent<DestructibleObject>(out DestructibleObject destructibleObject))
        {
            destructibleObject.MakeDamage(_damage);
        }
        
        EffectsManager.SetupExplosion(_explosionType, transform.position, Quaternion.identity);
        
        if (_pooledObject != null) _pooledObject.ReturnToPool();
    }
}
