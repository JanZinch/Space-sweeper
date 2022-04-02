using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    
    public static void SetupExplosion(Vector3 position, Quaternion rotation)
    { 
        PooledObject particles = PoolsManager.GetObject(PooledObjectType.FIREBALL_EXPLOSION, position, rotation);
        
        Context.DelayedCallback.Invoke(particles.GetComponent<ParticleSystem>().main.duration, () => { particles.ReturnToPool(); });
        
    }

}