using UnityEngine;
using System;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;
    public event Action OnObstacleHit = null;
    public event Action OnChannelHit = null;

    private Vector3 _fallForce = new Vector3(0.0f, -0.5f, 0.0f);
    
    public void Fall()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.AddRelativeForce(_fallForce, ForceMode.Force);
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
}
