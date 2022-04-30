using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    
    public static void SetupExplosion(PooledObjectType explosionType, Vector3 position, Quaternion rotation)
    { 
        PooledObject particles = PoolsManager.GetObject(explosionType, position, rotation);
        
        Context.DelayedCallback.Invoke(particles.GetComponent<ParticleSystem>().main.duration, () => { particles.ReturnToPool(); });
    }
}