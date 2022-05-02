using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AI {

    public class LinearActor : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rigidBody = null;
        [SerializeField] protected float _forwardSpeed = default;
        [SerializeField] private bool _setSpeedOnStart = false;
        [SerializeField] private OpposingActor _opposingBehaviour = null;

        private Vector3 _speed = default;
        
        public virtual void Initialize()
        {
            SetSpeed();
            if(_opposingBehaviour!=null) _opposingBehaviour.DistanceCheck = true;
        }

        protected virtual void Update()
        {
            transform.Translate(_speed * Time.deltaTime);
        }
        
        protected virtual void SetSpeed()
        {
            _speed = new Vector3(0.0f, 0.0f, _forwardSpeed);
            //_rigidBody.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);
        }
        
        protected virtual void SetSpeed(Vector3 speed)
        {
            _speed = speed;
            //_rigidBody.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);
        }

        public void UpdateSpeed(Vector3 append)
        {
            _speed += append;
        }

        protected virtual void Start()
        {
            if (_setSpeedOnStart) SetSpeed();

            _opposingBehaviour.OnSlow += UpdateSpeed;
            _opposingBehaviour.OnOvertake += () =>
            {
                _speed = new Vector3(_speed.x, _speed.y, GameManager.Instance.Player.GetForwardSpeed());
            };
        }
        
        public virtual void Clear()
        {
            if(_opposingBehaviour!=null) _opposingBehaviour.DistanceCheck = false;
        }
    }



}


