using UnityEngine;
using System;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;
    public event Action OnObstacleHit = null;
    public event Action OnChannelHit = null;
    
    public void Fall()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Projectile))
        {
            OnObstacleHit?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Channel))
        {
            OnChannelHit?.Invoke();
        }
    }

    /*private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Cylinder")
        {
            Debug.Log("Player hit " + collider.gameObject.name);
            OnObstacleHit?.Invoke();
            Destroy(collider.gameObject); // Put in object pooler
        }
    }*/
}
