using System;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private PoolsExplorer _poolsExplorer = null;
    
    private void Awake()
    {
        _poolsExplorer.Initialize();
        
    }
    
}
    
