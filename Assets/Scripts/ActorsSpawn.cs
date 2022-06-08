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
            AIController spawnedObject = PoolsManager.GetPooledObject(spawnPoint.Key, spawnPoint.Value.position, Quaternion.Euler(Vector3.zero))
                .GetLinkedComponent<AIController>();
            
            spawnedObject.SetStartEulerAngle(spawnPoint.Value.eulerAngles.z);
            spawnedObject.Initialize();
            
        }
    }
}