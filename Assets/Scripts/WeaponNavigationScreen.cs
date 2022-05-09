using System;
using UnityEngine;

public class WeaponNavigationScreen : MonoBehaviour
{
    public static WeaponNavigationScreen PlayerNavigationScreen { get; private set; } = null;
    private Transform _currentTarget = null;
    
    public event Action<Transform> OnNewTarget = null;


    public void SetForPlayer()
    {
        PlayerNavigationScreen = this;
    }

    public void OnTrigger(Transform caughtObject)
    {
        if (this == PlayerNavigationScreen)
        {
            if (_currentTarget == null)
            {
                _currentTarget = caughtObject.transform;
                OnNewTarget?.Invoke(_currentTarget);
            
                Debug.Log("New target: " + _currentTarget);
            }

        }
        else
        {
            if (_currentTarget == null && caughtObject.CompareTag(Tags.PlayerBody))
            {
                _currentTarget = caughtObject.transform;
                OnNewTarget?.Invoke(_currentTarget);
            
                Debug.Log("New target: " + _currentTarget);
            }
            
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