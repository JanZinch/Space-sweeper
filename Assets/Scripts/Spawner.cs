using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected List<Pair<PooledObjectType, Transform>> _spawnPoints = null;

    public virtual void Spawn()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            PoolsManager.GetPooledObject(spawnPoint.Key, spawnPoint.Value.position, Quaternion.Euler(Vector3.zero));
        }
    }

}