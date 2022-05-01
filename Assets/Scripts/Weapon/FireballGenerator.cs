using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballGenerator : Weapon
{
    [SerializeField] private Transform _sourcePosition = default;
    [SerializeField] private float _cooldown = 0.25f;
    
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
            PoolsManager.GetObject(PooledObjectType.FIREBALL, _sourcePosition.position, Quaternion.identity)
                .GetLinkedComponent<Projectile>().Setup(Vector3.forward);
            
            _deltaTime = 0.0f;
        }
    }
}


