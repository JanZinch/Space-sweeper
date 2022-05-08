using System;
using UnityEngine;

public class RocketLauncher : Weapon, INavigational
{
    [SerializeField] private Transform _sourcePoint = default;
    [SerializeField] private float _cooldown = 0.25f;
    [SerializeField] private float _navigationLerpDuration = 1.0f;
    
    private Transform _target = null;
    private float _deltaTime = default;

    private void Awake()
    {
        GetPlayerInput = Input.GetButtonDown;
    }

    private void Start()
    {
        _deltaTime = _cooldown;
        
    }

    public void Update()
    {
        _deltaTime += Time.deltaTime;
    }

    public override void FireIfPossible()
    {
        if (_deltaTime >= _cooldown)
        {
            PoolsManager.GetPooledObject(PooledObjectType.ROCKET, _sourcePoint.position, Quaternion.identity)
                .GetLinkedComponent<Projectile>().Setup(Vector3.forward).SetNavigation(_target, _navigationLerpDuration);
            
            _deltaTime = 0.0f;
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}