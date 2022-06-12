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
        PooledObject particlesPooled = PoolsManager.GetPooledObject(explosionType, position, rotation);
        ParticleSystem particleSystem = particlesPooled.GetLinkedComponent<ParticleSystem>();
        
        float explosionDuration = particleSystem.main.duration;
        particleSystem.Play(true);
        _instance.StartCoroutine(particlesPooled.ReturnToPool(explosionDuration));
    }

    public static PooledObject SetupParticles(PooledObjectType explosionType, Vector3 position, Quaternion rotation)
    {
        return PoolsManager.GetPooledObject(explosionType, position, rotation);
    }

}