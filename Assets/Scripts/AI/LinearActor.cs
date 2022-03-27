using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {

    public class LinearActor : MonoBehaviour
    {

        [SerializeField] protected float _forwardSpeed = default;
        [SerializeField] protected Rigidbody _rigidBody = null;

        protected Vector3? _startPosition = null;


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


