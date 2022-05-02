using System;
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
        
        private State _state = State.APPROXIMATION;
        
        
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
        
        private bool CheckDistanceFromPlayer(float distance)
        {
            return Mathf.Abs(transform.position.z - GameManager.Instance.Player.transform.position.z) <= distance;
        }

        private void FixedUpdate()
        {
            if (_state == State.CONFRONTATION) return;
            
            if (CheckDistanceFromPlayer(_distanceFromPlayer.Min))
            {
                _state = State.CONFRONTATION;
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
                Vector3 cachedVelocity = _rigidBody.velocity;
                _rigidBody.velocity = new Vector3(cachedVelocity.x, cachedVelocity.y, cachedVelocity.z +=_slowdown);
            }
        }
    }
}