using System;
using System.Collections;
using CodeBase.ApplicationLibrary.Common;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerBody _playerBody;

    [Header("Navigation_XZ")] 
    [SerializeField] private float _minSpeed = 5.0f;
    [SerializeField] private float _maxSpeed = 35.0f;
    [SerializeField] private float _startAcceleration = 10.0f, _fallAcceleration = 10.0f, _destroyedAcceleration = 5.0f;
    [SerializeField] private float _maxRotationSpeed = 70.0f, _maxRotationAngle = 10.0f;

    [Header("Navigation_Y")]
    [SerializeField] private float _maxYSpeed = 20.0f;
    [SerializeField] private float _minYSpeed = 5.0f;

    [Header("Boost")] 
    [SerializeField] private float _maxSpeedMultiplier = 2.0f;
    private float _currentSpeedMultiplier = 1.0f;
    
    private float _verticalSpeed = 0.0f;
    
    private float _forwardSpeed = 0.0f, _currentRotationSpeed = 0.0f;

    private bool _isFalls = false;
    private bool _isDestroyed = false;

    private Transform _playerScreen = null;
    private Transform _lowerPoint = default;
    
    public float ForwardSpeed =>_forwardSpeed;
    
    private void Awake()
    {
        _playerScreen = _playerBody.transform.parent;
    
        _lowerPoint = new GameObject("player_lower_point").transform;
        _lowerPoint.parent= transform;

        _lowerPoint.position = _playerBody.transform.position;

    }

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
        
        Messenger.Broadcast(MessengerKeys.ON_PLAYER_STARSHIP_FALL);
        _playerBody.Fall();
        StartCoroutine(DelayedRestart(5.0f));
    }

    private void OnChannelHit()
    {
        EffectsManager.SetupExplosion(PooledObjectType.FIREBALL_EXPLOSION, _playerBody.transform.position, Quaternion.identity);
        Destroy(_playerBody.gameObject);
        _isDestroyed = true;
        StartCoroutine(DelayedRestart(2.0f));
    }

    private IEnumerator DelayedRestart(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        SceneManager.Load(Scene.DOCK);
    }
    
    private Tween OnDeath()
    {
        _isFalls = true;
        EffectsManager.SetupExplosion(PooledObjectType.FIREBALL_EXPLOSION, _playerBody.transform.position, Quaternion.identity);
        
        Messenger.Broadcast(MessengerKeys.ON_PLAYER_STARSHIP_FALL);
        _playerBody.Fall();

        StartCoroutine(DelayedRestart(5.0f));
        
        return DOTween.Sequence();
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
        _currentRotationSpeed = (_isFalls) ? 0.0f : Input.GetAxis("Horizontal") * _maxRotationSpeed * _forwardSpeed / _maxSpeed;

        _verticalSpeed = Input.GetAxis("Vertical");

        _currentSpeedMultiplier = (Input.GetButton("Jump"))? _maxSpeedMultiplier : 1.0f;

    }

    private void MovePlayer()
    {
        if (!_isDestroyed)
        {
            Vector3 currentAngle = _playerBody.transform.localEulerAngles;
            transform.Translate(0f, 0f, _forwardSpeed * _currentSpeedMultiplier * Time.deltaTime);

            transform.Rotate(0f, 0f, _currentRotationSpeed * Time.deltaTime);
            _playerBody.transform.localEulerAngles = new Vector3(currentAngle.x, currentAngle.y, -_maxRotationAngle * Input.GetAxis("Horizontal"));
            
            MoveY();

        }
        else
        {
            transform.Translate(0f, 0f, _forwardSpeed * Time.deltaTime);
        }
    }

    private void MoveY()
    {
        Vector3 target;
        
        if (_verticalSpeed > 0.0f)
        {
            target = transform.position;
        }
        else if (_verticalSpeed < 0.0f)
        {
            _verticalSpeed *= -1;
            target = _lowerPoint.position;
        }
        else return;
        
        _playerScreen.transform.position = Vector3.MoveTowards(_playerScreen.transform.position, target, _verticalSpeed * _maxSpeed * Time.deltaTime);
        
    }



    private void SetMinSpeed()
    {
        _forwardSpeed = _minSpeed;
    }
}
