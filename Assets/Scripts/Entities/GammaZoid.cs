using System;
using DG.Tweening;
using Entities;
using UnityEngine;


public class GammaZoid : Enemy
{
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private DestructibleObject _destructibleObject = null;

    private Vector3 _fallForce = new Vector3(0.0f, -0.5f, 0.0f);
    
    private void OnEnable()
    {
        _destructibleObject.OnDeath += OnDeath;
    }

    private Tween OnDeath()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.AddRelativeForce(_fallForce, ForceMode.Force);
        
        PoolsManager.GetPooledObject(PooledObjectType.FIREBALL_EXPLOSION, transform.position, Quaternion.identity);

        Sequence s = DOTween.Sequence().SetDelay(2.0f);

        return s;
    }

    
}