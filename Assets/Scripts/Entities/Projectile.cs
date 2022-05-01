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
    [SerializeField] private PooledObject _pooledObject = null;
    
    
    public PooledObject Setup(Vector3 direction)
    {
        Setup(new Vector3(direction.x * _speed, direction.y * _speed, direction.z * _speed), _damage);
        return _pooledObject;
    }
    
    private void Setup(Vector3 velocity, int damage)
    {
        _rigidbody.velocity = velocity;
        _damage = damage;
        
        if(_trailParticles) _trailParticles.Play();
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
