using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AI {

    [RequireComponent(typeof(AIController))]
    public class LinearActor : AIBaseActor
    {
        [SerializeField] protected float _forwardSpeed = default;
        [SerializeField] private bool _setSpeedOnStart = false;

        private Vector3 _linearSpeed = default;

        public override void Initialize()
        {
            SetSpeed();
        }

        public override void Clear()
        {
            Stop();
        }

        protected virtual void Update()
        {
            transform.Translate(_linearSpeed * Time.deltaTime);
        }
        
        protected virtual void SetSpeed()
        {
            _linearSpeed = new Vector3(0.0f, 0.0f, _forwardSpeed);
        }

        protected void Stop()
        {
            _linearSpeed = Vector3.zero;
        }

        public void AppendSpeed(Vector3 append)
        {
            _linearSpeed += append;
        }

        public void SetForwardSpeed(float forwardSpeed)
        {
            _linearSpeed = new Vector3(_linearSpeed.x, _linearSpeed.y, forwardSpeed);
        }

        protected virtual void Start()
        {
            if (_setSpeedOnStart) SetSpeed();
        }
        
    }



}


