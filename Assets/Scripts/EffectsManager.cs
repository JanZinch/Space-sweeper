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
        PooledObject particles = PoolsManager.GetPooledObject(explosionType, position, rotation);

        float explosionDuration = particles.GetLinkedComponent<ParticleSystem>().main.duration;
        _instance.StartCoroutine(particles.ReturnToPool(explosionDuration));
    }

    public static PooledObject SetupParticles(PooledObjectType explosionType, Vector3 position, Quaternion rotation)
    {
        return PoolsManager.GetPooledObject(explosionType, position, rotation);
    }

}