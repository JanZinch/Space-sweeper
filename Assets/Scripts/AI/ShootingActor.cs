using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
    public class ShootingActor : AIBaseActor
    {
        [SerializeField] private WeaponController _weaponController = null;
        [SerializeField] private float _shootingDuration = 0.0f;
        [SerializeField] private float _minCooldown = 0.5f, _maxCooldown = 1.0f;

        private WaitForSeconds _cachedCooldown = null;
        private Coroutine _shootingRoutine = null;
        
        private void Awake()
        {
            if (Mathf.Approximately(_maxCooldown, _maxCooldown))
            {
                _cachedCooldown = new WaitForSeconds(_maxCooldown);
            }
        }

        private void OnEnable()
        {
            _weaponController.NavigationScreen.OnNewTarget += OnNewTarget;
            
        }

        private void OnDisable()
        {
            _weaponController.NavigationScreen.OnNewTarget -= OnNewTarget;
        }

        private void OnNewTarget(Transform target)
        {
            if (_shootingRoutine != null)
            {
                StopCoroutine(_shootingRoutine);
            }

            _shootingRoutine = StartCoroutine(FireOnTarget(target,_cachedCooldown != null));
        }

        private IEnumerator FireOnTarget(Transform target, bool cooldownIsCached)
        {
            while (true)
            {
                yield return (cooldownIsCached) ? _cachedCooldown 
                    : new WaitForSeconds(Random.Range(_minCooldown, _maxCooldown));
                
                _weaponController.FireToPositionIfPossible(0, target.position);

                yield return null;
            }
            
        }

        public override void Initialize() { }

        public override void Clear()
        {
            StopCoroutine(_shootingRoutine);
            _weaponController.NavigationScreen.Free();
        }
    }
}