using UnityEngine;
using System;

public class PlayerBody : MonoBehaviour
{
    public Action OnObstacleHit;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Cylinder")
        {
            Debug.Log("Player hit " + collider.gameObject.name);
            OnObstacleHit?.Invoke();
            Destroy(collider.gameObject); // Put in object pooler
        }
    }
}
