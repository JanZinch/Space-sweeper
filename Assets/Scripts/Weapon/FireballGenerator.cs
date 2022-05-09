using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballGenerator : Weapon
{
    [SerializeField] private Transform _sourcePoint = default;
    [SerializeField] private float _cooldown = 0.25f;
    
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
            PoolsManager.GetPooledObject(PooledObjectType.FIREBALL, _sourcePoint.position, Quaternion.identity)
                .GetLinkedComponent<Projectile>().Setup(Vector3.forward);
            
            _deltaTime = 0.0f;

            return true;
        }
        else return false;
    }
}


