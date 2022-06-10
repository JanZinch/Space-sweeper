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
        private WaitForSeconds _cachedShootingWait = null;
        private Coroutine _shootingRoutine = null;
        
        private void Awake()
        {
            if (ApproximatelyEquals(_minCooldown, _maxCooldown))
            {
                _cachedCooldown = new WaitForSeconds(_maxCooldown);
            }

            _cachedShootingWait = new WaitForSeconds(_shootingDuration);
        }

        private bool ApproximatelyEquals(float a, float b, float eps = 0.0001f)
        {
            return Mathf.Abs(a - b) < eps;
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

            Debug.Log("Cooldown is cached: " + (_cachedCooldown != null));
            
            if (_weaponController.IsLaserEmitter(0))
            {
                _shootingRoutine = StartCoroutine(UseLaser(_cachedCooldown != null));
            }
            else
            {
                _shootingRoutine = StartCoroutine(FireOnTarget(target,_cachedCooldown != null));
            }
        }

        private IEnumerator FireOnTarget(Transform target, bool cooldownIsCached)
        {
            while (true)
            {
                yield return (cooldownIsCached) ? _cachedCooldown 
                    : new WaitForSeconds(Random.Range(_minCooldown, _maxCooldown));

                if (target != null)
                {
                    _weaponController.FireToPositionIfPossible(0, target.position);
                }
                
                yield return null;
            }
        }

        private IEnumerator UseLaser(bool cooldownIsCached)
        {
            _weaponController.SetShootingDurationIfNeed(_shootingDuration);
            
            while (true)
            {
                yield return (cooldownIsCached) ? _cachedCooldown 
                    : new WaitForSeconds(Random.Range(_minCooldown, _maxCooldown));
                
                _weaponController.FireByDirectionIfPossible(0, Vector3.back);
                
                yield return _cachedShootingWait;

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