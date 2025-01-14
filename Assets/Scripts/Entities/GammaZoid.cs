﻿using DG.Tweening;
using Entities;
using UnityEngine;

public class GammaZoid : Enemy
{
    [SerializeField] private Rigidbody _rigidbody = null;
    private Vector3 _fallForce = new Vector3(0.0f, -0.5f, 0.0f);

    private void OnEnable()
    {
        _destructibleObject.OnDeath += OnDeath;
        _destructibleObject.OnRefresh += OnRefresh;
    }

    private Tween OnDeath()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.AddRelativeForce(_fallForce, ForceMode.Force);
        
        Explode();
        
        return DOTween.Sequence().SetDelay(2.0f).OnComplete(Explode);
    }

    private void Explode()
    {
        EffectsManager.SetupExplosion(PooledObjectType.GAMMAZOID_EXPLOSION, transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        _destructibleObject.OnDeath -= OnDeath;
        _destructibleObject.OnRefresh -= OnRefresh;
    }
    
    private void OnRefresh()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _rigidbody.velocity = Vector3.zero;
    }
    
    

}