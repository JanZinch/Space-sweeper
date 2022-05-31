using System;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class ActorsSpawn : MonoBehaviour
{
    [SerializeField] private List<Pair<PooledObjectType, Transform>> _spawnPoints = null;

    public void Spawn()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            PooledObject spawnedObject = PoolsManager.GetPooledObject(spawnPoint.Key, spawnPoint.Value.position, spawnPoint.Value.rotation);
            spawnedObject.GetLinkedComponent<AIController>().Initialize();
        }
    }
}