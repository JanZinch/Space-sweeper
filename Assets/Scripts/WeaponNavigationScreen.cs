using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNavigationScreen : MonoBehaviour
{
    public static WeaponNavigationScreen PlayerNavigationScreen { get; private set; } = null;
    private Transform _currentTarget = null;
    private Queue<Transform> _potentialTargets = null;

    public event Action<Transform> OnNewTarget = null;

    private void Awake()
    {
        _potentialTargets = new Queue<Transform>();
    }
    
    public void SetForPlayer()
    {
        PlayerNavigationScreen = this;
    }

    public void OnTrigger(Transform caughtObject, bool isPlayer = false)
    {
        if (this == PlayerNavigationScreen)
        {
            if (_currentTarget == null)
            {
                _currentTarget = caughtObject;
                OnNewTarget?.Invoke(_currentTarget);
            }
            else
            {
                if (!_potentialTargets.Contains(caughtObject))
                {
                    _potentialTargets.Enqueue(caughtObject);
                }
            }

        }
        else if (isPlayer)
        {
            _currentTarget = caughtObject;
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
            if (_potentialTargets.Count > 0)
            {
                _currentTarget = _potentialTargets.Dequeue();
            }
            else _currentTarget = null;
            
            OnNewTarget?.Invoke(_currentTarget);
        }
    }

    public Transform GetCurrentTarget()
    {
        return _currentTarget;
    }
}