using System;
using UnityEngine;

public class WeaponNavigationScreen : MonoBehaviour
{
    public static WeaponNavigationScreen Instance { get; private set; } = null;
    private Transform _currentTarget = null;
    
    public event Action<Transform> OnNewTarget = null;

    
    
    private void Awake()
    {
        Instance = this;
    }

    public void OnTrigger(Transform caughtObject)
    {
        if (_currentTarget == null)
        {
            _currentTarget = caughtObject.transform;
            OnNewTarget?.Invoke(_currentTarget);
            
            Debug.Log("New target: " + _currentTarget);
        }
    }

    public void Free()
    {
        _currentTarget = null;
    }
    
    public void FreeIfNeed(Transform target)
    {
        if (_currentTarget == target)
        {
            Debug.Log("FREE");
            _currentTarget = null;
            OnNewTarget?.Invoke(_currentTarget);
        }
    }

    public Transform GetCurrentTarget()
    {
        return _currentTarget;
    }
}