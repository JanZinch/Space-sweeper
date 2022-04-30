using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballGenerator : MonoBehaviour
{
    [SerializeField] private Transform _sourcePosition = default;
    [SerializeField] private float _cooldown = 1.0f;
    
    private const string Fire1 = "Fire1";

    private float _deltaTime = default;

    private void Start()
    {
        _deltaTime = _cooldown;
    }

    public void Update()
    {
        if (Input.GetButtonDown(Fire1) && _deltaTime >= _cooldown)
        {
            PoolsManager.GetObject(PooledObjectType.FIREBALL, _sourcePosition.position, Quaternion.identity);
            _deltaTime = 0.0f;
        }

        _deltaTime += Time.deltaTime;
    }

}
