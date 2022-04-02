using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private ParticleSystem _trailParticles = null;
    [SerializeField] private PooledObject _pooledObject = null;
    
    private double _damage = 0.0;

    private void Start()
    {
        Setup(new Vector3(0.0f, 0.0f, 10.0f), 10.0f);
    }

    private void Setup(Vector3 velocity, double damage)
    {
        _rigidbody.velocity = velocity;
        _damage = damage;
        
        if(_trailParticles) _trailParticles.Play();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<DestructibleObject>(out DestructibleObject destructibleObject))
        {
            destructibleObject.MakeDamage(_damage);

            // effects setup
            Debug.Log("Collision");
            
            EffectsManager.SetupExplosion(transform.position, Quaternion.identity);
            
            if (_pooledObject != null) _pooledObject.ReturnToPool();

            
        }
    }
}
