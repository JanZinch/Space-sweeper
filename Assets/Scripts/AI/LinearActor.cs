using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {

    public class LinearActor : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rigidBody = null;
        [SerializeField] protected float _forwardSpeed = default;
        [SerializeField] private bool _setSpeedOnStart = false;
        [SerializeField] private OpposingActor _opposingBehaviour = null;

        public virtual void Initialize()
        {
            SetSpeed();
            if(_opposingBehaviour!=null) _opposingBehaviour.DistanceCheck = true;
        }

        protected virtual void SetSpeed() {

            _rigidBody.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);
        }
        
        protected virtual void Start()
        {
            if (_setSpeedOnStart) SetSpeed();            
        }
        
        public virtual void Clear()
        {
            if(_opposingBehaviour!=null) _opposingBehaviour.DistanceCheck = false;
        }
    }



}


