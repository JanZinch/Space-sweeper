using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {

    public class LinearActor : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rigidBody = null;
        [SerializeField] protected float _forwardSpeed = default;
        [SerializeField] protected float _slowdown = 1.0f;
        [SerializeField] protected DistanceFromPlayer _distanceFromPlayer = new DistanceFromPlayer(50.0f, 10.0f);
        
        protected Vector3? _startPosition = null;

        private State _state = State.APPROXIMATION;
        
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

        private enum State
        {
            APPROXIMATION = 0, SLOWDOWN = 1, PERSECUTION = 2
        }

        protected virtual void OnEnable()
        {
            if (_startPosition != null)
            {

                if (transform.parent != null)
                {
                    transform.localPosition = (Vector3)_startPosition;
                }
                else
                {

                    transform.position = (Vector3)_startPosition;
                }
            }
        }

        protected virtual void SetSpeed() {

            _rigidBody.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);
        }


        private bool CheckDistanceFromPlayer(float distance)
        {
            return Mathf.Abs(transform.position.z - GameManager.Instance.Player.transform.position.z) <= distance;
        }

        private void FixedUpdate()
        {
            if (_state != State.PERSECUTION && CheckDistanceFromPlayer(_distanceFromPlayer.Min))
            {
                _state = State.PERSECUTION;
                GameManager.Instance.Player.Overtake(_rigidBody);
            }
            else if (CheckDistanceFromPlayer(_distanceFromPlayer.Slowdown))
            {
                _state = State.SLOWDOWN;
            }

        }

        private void Update()
        {
            if (_state == State.SLOWDOWN)
            {
                _rigidBody.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);
            }
        }

        protected void Start()
        {
            if (transform.parent != null)
            {
                _startPosition = transform.localPosition;
            }
            else
            {

                _startPosition = transform.position;
            }

            SetSpeed();            
        }


    }



}


