using System;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class ActorsSpawn : MonoBehaviour
{
    private List<KeyValuePair<PooledObjectType, Transform>> _spawnPoints = null;
    
    private void OnEnable()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            PoolsManager.GetPooledObject(spawnPoint.Key, spawnPoint.Value.position, Quaternion.identity);
        }
    }
}