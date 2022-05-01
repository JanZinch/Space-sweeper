using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerBody _playerBody;

    [Header("Navigation")] 
    [SerializeField] private float _minSpeed = 5.0f;
    [SerializeField] private float _maxSpeed = 35.0f;
    [SerializeField] private float _startAcceleration = 10.0f, _fallAcceleration = 10.0f, _destroyedAcceleration = 5.0f;
    [SerializeField] private float  _maxRotationSpeed = 70.0f, _maxRotationAngle = 10.0f;

    private float _forwardSpeed = 0.0f, _currentRotationSpeed = 0.0f;

    private bool _isFalls = false;
    private bool _isDestroyed = false;
    
    private void OnEnable()
    {
        _playerBody.OnObstacleHit += OnObstacleHit;
        _playerBody.OnChannelHit += OnChannelHit;
    }

    private void Start()
    {
        SetMinSpeed();
    }

    private void Update()
    {
        Accelerate();
        GetInput();
        MovePlayer();
    }

    private void OnDisable()
    {
        _playerBody.OnObstacleHit -= OnObstacleHit;
        _playerBody.OnChannelHit -= OnChannelHit;
    }

    private void OnObstacleHit()
    {
        _isFalls = true;
        EffectsManager.SetupExplosion(PooledObjectType.FIREBALL_EXPLOSION, _playerBody.transform.position, Quaternion.identity);
        _playerBody.Fall();
    }

    private void OnChannelHit()
    {
        EffectsManager.SetupExplosion(PooledObjectType.FIREBALL_EXPLOSION, _playerBody.transform.position, Quaternion.identity);
        Destroy(_playerBody.gameObject);
        _isDestroyed = true;
    }

    private void Accelerate()
    {
        if (_isDestroyed && _forwardSpeed > 0.0f)
        {
            _forwardSpeed -= _destroyedAcceleration * Time.deltaTime;
        }
        else if (_isFalls && _forwardSpeed > _minSpeed)
        {
            _forwardSpeed -= _fallAcceleration * Time.deltaTime;
        }
        else if (_forwardSpeed < _maxSpeed)
        {
            _forwardSpeed += _startAcceleration * Time.deltaTime;
        }
    }

    private void GetInput()
    {
        _currentRotationSpeed = (_isFalls) ? 0.0f : Input.GetAxisRaw("Horizontal") * _maxRotationSpeed * _forwardSpeed / _maxSpeed;
    }

    private void MovePlayer()
    {
        if (!_isDestroyed)
        {
            Vector3 currentAngle = _playerBody.transform.localEulerAngles;

            transform.Translate(0f, 0f, _forwardSpeed * Time.deltaTime);
            //Debug.Log("Speed: " + _forwardSpeed);
            transform.Rotate(0f, 0f, _currentRotationSpeed * Time.deltaTime);
            _playerBody.transform.localEulerAngles = Vector3.right * currentAngle.x + Vector3.up * currentAngle.y + 
                                                     -Vector3.forward * _maxRotationAngle * _currentRotationSpeed / _maxRotationSpeed;
        }
        else
        {
            transform.Translate(0f, 0f, _forwardSpeed * Time.deltaTime);
        }


    }

    private void SetMinSpeed()
    {
        _forwardSpeed = _minSpeed;
    }

    public void Overtake(Rigidbody actor)
    {
        Vector3 cachedVelocity = actor.velocity;
        actor.velocity = new Vector3(0.0f, 0.0f, _forwardSpeed);
    }
}
