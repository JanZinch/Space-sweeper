using System;
using Entities;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private float _speed = 100.0f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private ParticleSystem _trailParticles = null;
    [SerializeField] private PooledObject _pooledObject = null;

    private void Awake()
    {
        _pooledObject.Setup = Setup;
    }

    private void Start()
    {
        //Setup(new Vector3(0.0f, 0.0f, _speed), _damage);
    }

    public PooledObject Setup()
    {
        Setup(new Vector3(0.0f, 0.0f, _speed), _damage);
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
        
        
        
        EffectsManager.SetupExplosion(PooledObjectType.FIREBALL_EXPLOSION, transform.position, Quaternion.identity);
        
        if (_pooledObject != null) _pooledObject.ReturnToPool();
    }
}
