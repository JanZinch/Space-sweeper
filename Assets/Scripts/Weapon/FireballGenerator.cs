using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballGenerator : MonoBehaviour
{
    [SerializeField] private Transform _sourcePosition = default;
    
    private const string Fire1 = "Fire1";
    
    public void Update()
    {
        if (Input.GetButtonDown(Fire1))
        {
            PoolsManager.GetObject(PooledObjectType.FIREBALL, _sourcePosition.position, Quaternion.identity);
        }

        

    }

}
