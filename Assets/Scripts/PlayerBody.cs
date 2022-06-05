using UnityEngine;
using System;
using DG.Tweening;
using Entities;
using Utils;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private DestructibleObject _destructible = null;
    
    public event Action OnObstacleHit = null;
    public event Action OnChannelHit = null;
    public event Action<int> OnHealthUpdate = null;
    public event Action OnDeath = null;
    
    private Vector3 _fallForce = new Vector3(0.0f, -0.5f, 0.0f);

    private void Awake()
    {
        _destructible.SetMaxHealth(PlayerUtils.MaxHealth);
    }

    private void OnEnable()
    {
        _destructible.OnDeath += OnDeathInvoke;
        _destructible.OnHealthUpdate += OnHealthUpdateInvoke;
    }

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
        else if (other.TryGetComponent<WeaponNavigationScreen>(out WeaponNavigationScreen weaponNavigationScreen))
        {
            weaponNavigationScreen.OnTrigger(transform, true);
        }
        
    }

    private void OnHealthUpdateInvoke(int health)
    {
        OnHealthUpdate?.Invoke(health);
    }
    
    private Tween OnDeathInvoke()
    {
        OnDeath?.Invoke();
        return null;
    }

    private void OnDisable()
    {
        _destructible.OnDeath += OnDeathInvoke;
        _destructible.OnHealthUpdate -= OnHealthUpdateInvoke;
    }
}
