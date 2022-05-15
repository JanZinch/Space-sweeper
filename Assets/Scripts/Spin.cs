using UnityEngine;

namespace Assets.Scripts
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] private float angularSpeed = default;
        
        private void Update()
        {
            transform.Rotate(Vector3.forward, angularSpeed * Time.deltaTime, Space.Self);
        }
    }
}