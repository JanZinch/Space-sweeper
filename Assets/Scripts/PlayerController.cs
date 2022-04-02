using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerBody _playerBody;
    
    [Header("Navigation")]
    [SerializeField] private float _minSpeed = 1.0f, _maxSpeed = 50.0f;
    [SerializeField] private float _acceleration = 20.0f;
    [SerializeField] private float  _maxRotationSpeed = 70.0f, _maxRotationAngle = 25.0f;

    [Space] 
    [SerializeField] private FireballGenerator _fireballGenerator = null;
    
    private float _forwardSpeed = 0.0f, _currentRotationSpeed = 0.0f;
    
    private void OnEnable()
    {
        _playerBody.OnObstacleHit += SetMinSpeed;
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

    private void OnDisable()
    {
        _playerBody.OnObstacleHit -= SetMinSpeed;
    }
    


    private void Acceleration()
    {
        if (_forwardSpeed < _maxSpeed)
        {
            _forwardSpeed += _acceleration * Time.deltaTime;
        }
    }

    private void GetInput()
    {
        _currentRotationSpeed = Input.GetAxisRaw("Horizontal") * _maxRotationSpeed * _forwardSpeed / _maxSpeed;
    }

    private void MovePlayer()
    {
        Vector3 currentAngle = _playerBody.transform.localEulerAngles;

        transform.Translate(0f, 0f, _forwardSpeed * Time.deltaTime);
        //Debug.Log("Speed: " + _forwardSpeed);
        transform.Rotate(0f, 0f, _currentRotationSpeed * Time.deltaTime);
        _playerBody.transform.localEulerAngles = Vector3.right * currentAngle.x + Vector3.up * currentAngle.y + 
            -Vector3.forward * _maxRotationAngle * _currentRotationSpeed / _maxRotationSpeed;
    }

    private void SetMinSpeed()
    {
        _forwardSpeed = _minSpeed;
    }
}
