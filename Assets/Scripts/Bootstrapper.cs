using System;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Start()
    {
        Debug.Log(Context.DataHelper.GetLong("TEST"));
    }
}
    
