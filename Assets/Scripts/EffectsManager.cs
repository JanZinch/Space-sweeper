using System;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    private static EffectsManager _instance = null;
    
    public void Initialize()
    {
        _instance = this;
    }

    public static void SetupExplosion(PooledObjectType explosionType, Vector3 position, Quaternion rotation)
    { 
        PooledObject particles = PoolsManager.GetObject(explosionType, position, rotation);

        float explosionDuration = particles.GetComponent<ParticleSystem>().main.duration;
        _instance.StartCoroutine(particles.ReturnToPool(explosionDuration));
    }
}