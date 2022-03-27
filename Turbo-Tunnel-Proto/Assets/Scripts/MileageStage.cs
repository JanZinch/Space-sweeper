using System;
using UnityEngine;


public class MileageStage : MonoBehaviour
{
    public float Size { get; set; } = 100.0f;
    private float _start = default;
    private float _end = default;

    public Action OnNextStage = null;

    void Start()
    {
        _start = transform.position.z;
    }

    void Update()
    {

        if (Mathf.Abs(_end - _start) >= Size)
        {
            _start = transform.position.z;
            _end = transform.position.z;

            OnNextStage?.Invoke();
        }
        else
        {

            _end = transform.position.z;
        }

    }
}

