using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(AIController))]
    public class HelicalActor : LinearActor
    {
        [Space]
        [SerializeField] protected float _angularSpeed = default;
        [SerializeField] protected Transform _rotationPivot = null;

        private Vector3 _cachedSelfPosition = default;
        
        public override void Initialize()
        {
            base.Initialize();

            if (_cachedSelfPosition != transform.position && transform.position != default)
            {
                _cachedSelfPosition = transform.position;
                transform.DOMoveY(_cachedSelfPosition.y - Mathf.Abs(_rotationPivot.position.y - _cachedSelfPosition.y), 0.0f);
            }
        }

        protected override void Update()
        {
            base.Update();
            
            //RotateRigidBodyAroundPointBy(_rigidBody, _rotationPivot.position, Vector3.forward, _angularSpeed * Time.deltaTime);
            
            
            transform.RotateAround(_rotationPivot.transform.position, Vector3.forward, _angularSpeed * Time.deltaTime);
            //_centerOfMass.Rotate(new Vector3(0.0f, 0.0f, _angularSpeed * Time.deltaTime), Space.Self);
        }
        
        
        public override void Clear()
        {
            base.Clear();
            DOTween.Kill(transform);
        }
    }


}