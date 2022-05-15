﻿using System;
using Entities;
using UnityEngine;

public class LaserEmitter : Weapon
{
    [SerializeField] private Transform _sourcePoint = default;
    
    private Laser _activeLaser = null;
    private bool _isActive = false;

    public override bool GetPlayerInput(string buttonName)
    {
        bool input = Input.GetButton(buttonName);

        if (_isActive && !input)
        {
            _activeLaser.TurnOff();
            _activeLaser = null;
        }

        _isActive = input;

        return input;
    }

    public override bool FireIfPossible()
    {
        if (_activeLaser == null)
        {
            _activeLaser = PoolsManager.GetPooledObject(PooledObjectType.LASER, _sourcePoint.position, Quaternion.identity)
                .GetLinkedComponent<Laser>().SetSourcePoint(_sourcePoint);
            
            _activeLaser.transform.parent = transform;
            _activeLaser.TurnOn();

            _isActive = true;
        }

        return true;
    }

    public override void SetCustomTargetPosition(Vector3 targetPosition)
    {
        throw new NotImplementedException();
    }
}