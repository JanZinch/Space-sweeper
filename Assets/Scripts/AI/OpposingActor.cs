using System;
using System.Collections;
using AI;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(LinearActor))]
    public class OpposingActor : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rigidBody = null;
        [SerializeField] protected float _slowdown = 1.0f;
        [SerializeField] protected DistanceFromPlayer _distanceFromPlayer = new DistanceFromPlayer(50.0f, 10.0f);

        private bool _distanceCheck = false;
        
        public bool DistanceCheck
        {
            get
            {
                return _distanceCheck;
            }

            set
            {
                if (!_distanceCheck && value)
                {
                    _checkDistanceRoutine = StartCoroutine(CheckDistanceFromPlayer());
                }
                else if (_distanceCheck && !value){
                    
                    StopCoroutine(_checkDistanceRoutine);
                }
                
                _distanceCheck = value;
            }

        }

        private State _state = State.APPROXIMATION;
        private Coroutine _checkDistanceRoutine = null;
        private WaitForSeconds _checkDistanceDeltaTime = null;
        
        private enum State
        {
            APPROXIMATION = 0, SLOWDOWN = 1, CONFRONTATION = 2
        }
        
        [Serializable]
        public struct DistanceFromPlayer
        {
            public float Slowdown;
            public float Min;

            public DistanceFromPlayer(float slowdown, float min)
            {
                Slowdown = slowdown;
                Min = min;
            }
        }

        private void Start()
        {
            _checkDistanceDeltaTime = new WaitForSeconds(0.2f);
        }
        

        private IEnumerator CheckDistanceFromPlayer()
        {
            while (_state != State.CONFRONTATION)
            {
                if (CheckDistanceFromPlayer(_distanceFromPlayer.Min))
                {
                    _state = State.CONFRONTATION;
                    GameManager.Instance.Player.Overtake(_rigidBody);
                }
                else if (CheckDistanceFromPlayer(_distanceFromPlayer.Slowdown))
                {
                    _state = State.SLOWDOWN;
                }

                yield return _checkDistanceDeltaTime;
            }
        }

        private bool CheckDistanceFromPlayer(float distance)
        {
            return Mathf.Abs(transform.position.z - GameManager.Instance.Player.transform.position.z) <= distance;
        }
        
        
        private void Update()
        {
            if (_state == State.SLOWDOWN)
            {
                Vector3 cachedVelocity = _rigidBody.velocity;
                _rigidBody.velocity = new Vector3(cachedVelocity.x, cachedVelocity.y, cachedVelocity.z +=_slowdown);
            }
        }
        
    }
}