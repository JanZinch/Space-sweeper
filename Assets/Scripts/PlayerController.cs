using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private PlayerBody playerBodyCollision;
    [SerializeField] private float minSpeed = 1.0f, maxSpeed = 25.0f, accel = 10.0f, maxRotationSpeed = 20.0f, maxRotationAngle = 25.0f;

    private float _forwardSpeed = 0.0f, _currentRotationSpeed = 0.0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        playerBodyCollision.OnObstacleHit += SetMinSpeed;
    }

    private void Start()
    {
        SetMinSpeed();
    }

    private void Update()
    {
        Acceleration();
        GetInput();
        MovePlayer();
    }

    private void OnDestroy()
    {
        playerBodyCollision.OnObstacleHit -= SetMinSpeed;
    }


    private void Acceleration()
    {
        if (_forwardSpeed < maxSpeed)
        {
            _forwardSpeed += accel * Time.deltaTime;
        }
    }

    private void GetInput()
    {
        _currentRotationSpeed = Input.GetAxisRaw("Horizontal") * maxRotationSpeed * _forwardSpeed / maxSpeed;
    }

    private void MovePlayer()
    {
        Vector3 currentAngle = playerBodyCollision.transform.localEulerAngles;

        transform.Translate(0f, 0f, _forwardSpeed * Time.deltaTime);
        //Debug.Log("Speed: " + _forwardSpeed);
        transform.Rotate(0f, 0f, _currentRotationSpeed * Time.deltaTime);
        playerBodyCollision.transform.localEulerAngles = Vector3.right * currentAngle.x + Vector3.up * currentAngle.y + 
            -Vector3.forward * maxRotationAngle * _currentRotationSpeed / maxRotationSpeed;
    }

    private void SetMinSpeed()
    {
        _forwardSpeed = minSpeed;
    }
}
