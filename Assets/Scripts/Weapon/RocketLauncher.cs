using System;
using UnityEngine;

public class RocketLauncher : Weapon, INavigational
{
    [SerializeField] private Transform _sourcePoint = default;
    [SerializeField] private float _cooldown = 0.25f;
    [SerializeField] private float _navigationLerpDuration = 1.0f;
    
    private Transform _target = null;
    private float _deltaTime = default;
    
    private void Start()
    {
        _deltaTime = _cooldown;
        
    }

    public void Update()
    {
        _deltaTime += Time.deltaTime;
    }

    public override bool GetPlayerInput(string buttonName)
    {
        return Input.GetButtonDown(buttonName);
    }

    public override bool FireIfPossible()
    {
        if (_deltaTime >= _cooldown)
        {
            PoolsManager.GetPooledObject(PooledObjectType.ROCKET, _sourcePoint.position, Quaternion.identity)
                .GetLinkedComponent<Projectile>().Setup(Vector3.forward).SetNavigation(_target, _navigationLerpDuration);

            _deltaTime = 0.0f;
            
            return true;
        }
        else return false;
    }

    public override void SetCustomTargetPosition(Vector3 targetPosition)
    {
        throw new NotImplementedException();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}