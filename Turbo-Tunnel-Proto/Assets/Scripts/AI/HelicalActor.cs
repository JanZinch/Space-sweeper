using System.Collections;
using UnityEngine;

namespace AI
{

    public class HelicalActor : LinearActor
    {
        [SerializeField] protected float _angularSpeed = default;
        [SerializeField] protected Transform _centerOfMass = null;

        private void Update()
        {
            _centerOfMass.Rotate(new Vector3(0.0f, 0.0f, _angularSpeed * Time.deltaTime), Space.Self);
        }


    }


}