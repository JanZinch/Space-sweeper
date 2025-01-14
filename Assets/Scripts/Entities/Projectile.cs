using System;
using Entities;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private PooledObjectType _explosionType = PooledObjectType.FIREBALL_EXPLOSION;
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private float _speed = 100.0f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private ParticleSystem _trailParticles = null;
    [SerializeField] private TrailRenderer _trailRenderer = null;
    [SerializeField] private PooledObject _pooledObject = null;

    private Navigation _navigation = null;
    private Collider _collider = null;
        
    private class Navigation
    {
        private Transform _target = null;
        private readonly float _lerpDuration; 
        private Vector3 _startPosition = default;
        private float _currentLerpTime = 0.0f;
        
        public Transform Target => _target;

        public Navigation(Vector3 startPosition, Transform target, float lerpDuration)
        {
            _startPosition = startPosition;
            _lerpDuration = lerpDuration;
            _target = target;
            _currentLerpTime = 0.0f;
        }
        
        public Vector3 Update()
        {
            if (_target == null)
            {
                throw new Exception("Navigation target is null");
            }
            
            _currentLerpTime = Mathf.Clamp(_currentLerpTime + Time.deltaTime, 0.0f, _lerpDuration);
            
            float percentComplete = _currentLerpTime / _lerpDuration;
            return Vector3.Lerp(_startPosition, _target.position, percentComplete);
        }

    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public Projectile Setup(Vector3 direction)
    {
        Setup(direction * _speed, _damage);
        return this;
    }

    public Projectile SetNavigation(Transform target, float lerpDuration)
    {
        _navigation = new Navigation(transform.position, target, lerpDuration);
        return this;
    }

    private void Setup(Vector3 velocity, int damage)
    {
        _rigidbody.velocity = velocity;
        _damage = damage;
        
        if(_trailParticles) _trailParticles.Play();
    }

    private void Update()
    {
        if (_navigation != null && _navigation.Target != null)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.MovePosition(_navigation.Update());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_navigation != null)
        {
            Debug.Log("Rocket collision with: " + other.gameObject.name);
        }

        if (other.gameObject.TryGetComponent<DestructibleObject>(out DestructibleObject destructibleObject))
        {
            destructibleObject.MakeDamage(_damage);
        }
        
        EffectsManager.SetupExplosion(_explosionType, transform.position, Quaternion.identity);

        if (_trailRenderer)
        {
            _trailRenderer.Clear();
        }

        if (_pooledObject != null) _pooledObject.ReturnToPool();
    }

    public static void IgnoreCollision(Projectile first, Projectile second)
    {
        Physics.IgnoreCollision(first._collider, second._collider);
    }
}
