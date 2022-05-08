using System;
using UnityEngine;

public class WeaponNavigationScreen : MonoBehaviour
{
    private Transform _currentTarget = null;
    public event Action<Transform> OnNewTarget = null;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GammaZoid>(out GammaZoid enemy))
        {
            if (_currentTarget == null)
            {
                _currentTarget = enemy.transform;
                OnNewTarget?.Invoke(_currentTarget);
            }
        }
    }

    public Transform GetCurrentTarget()
    {
        return _currentTarget;
    }
}