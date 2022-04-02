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

    private void Start()
    {
        Setup(new Vector3(0.0f, 0.0f, _speed), _damage);
    }

    private void Setup(Vector3 velocity, int damage)
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
