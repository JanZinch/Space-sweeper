using System;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private PoolsExplorer _poolsExplorer = null;
    
    public void InitializePools()
    {
        _poolsExplorer.Initialize();
    }

}
    
