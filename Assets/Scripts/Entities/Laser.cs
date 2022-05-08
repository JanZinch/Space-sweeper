using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour
    {
        [SerializeField] private PooledObjectType _explosionType = PooledObjectType.FIREBALL_EXPLOSION;
        [SerializeField] private LineRenderer _lineRenderer = null;
        [SerializeField] private float _maxWidth = 1.0f;
        [SerializeField] private int _damage = 10;
        [SerializeField] private PooledObject _pooledObject = null;

        public void TurnOn()
        {
            _lineRenderer.startWidth = _maxWidth;
            _lineRenderer.endWidth = 0.5f * _maxWidth;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision with: " + other.gameObject.name);
        
            if (other.gameObject.TryGetComponent<DestructibleObject>(out DestructibleObject destructibleObject))
            {
                destructibleObject.MakeDamage(_damage);
            }
        
            EffectsManager.SetupExplosion(_explosionType, transform.position, Quaternion.identity);
        }

        public void TurnOff()
        {
            _lineRenderer.startWidth = _lineRenderer.endWidth = 0.0f;
            
            if (_pooledObject != null) _pooledObject.ReturnToPool();
        }

    }
}