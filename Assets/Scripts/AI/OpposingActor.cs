using System;
using System.Collections;
using AI;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(AIController))]
    [RequireComponent(typeof(LinearActor))]
    public class OpposingActor : AIBaseActor
    {
        [SerializeField] protected float _slowdown = 1.0f;
        [SerializeField] protected DistanceFromPlayer _distanceFromPlayer = new DistanceFromPlayer(50.0f, 10.0f);
        [SerializeField] protected LinearActor _linearActor = null;
        
        private bool _distanceCheck = false;

        private State _state = State.APPROXIMATION;
        private Coroutine _checkDistanceRoutine = null;
        private WaitForSeconds _checkDistanceDeltaTime = null;
        private Vector3 _slowdownAppend = default;

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
                
                else if (_distanceCheck && !value)
                {
                    StopCoroutine(_checkDistanceRoutine);
                }
                
                _distanceCheck = value;
            }
        }
        
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
            _slowdownAppend = new Vector3(0.0f, 0.0f, _slowdown);
            _checkDistanceDeltaTime = new WaitForSeconds(0.2f);
            
        }

        private IEnumerator CheckDistanceFromPlayer()
        {
            while (_state != State.CONFRONTATION)
            {
                if (CheckDistanceFromPlayer(_distanceFromPlayer.Min))
                {
                    _state = State.CONFRONTATION;
                    _linearActor.SetForwardSpeed(GameManager.Instance.Player.ForwardSpeed);
                }
                else if (CheckDistanceFromPlayer(_distanceFromPlayer.Slowdown))
                {
                    _state = State.SLOWDOWN;
                    _linearActor.AppendSpeed(_slowdownAppend);
                }

                yield return _checkDistanceDeltaTime;
            }
        }

        private bool CheckDistanceFromPlayer(float distance)
        {
            return Mathf.Abs(transform.position.z - GameManager.Instance.Player.transform.position.z) <= distance;
        }

        public override void Initialize()
        {
            DistanceCheck = true;
            _state = State.APPROXIMATION;
        }

        public override void Clear()
        {
            DistanceCheck = false;
            _state = State.APPROXIMATION;
            
        }
    }
}