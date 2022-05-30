using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MachineGun : Weapon
{
    [SerializeField] private List<Transform> _leftSourcePoints = default;
    [SerializeField] private List<Transform> _rightSourcePoints = default;
    [SerializeField] private float _cooldown = 0.1f;

    private Vector3 _leftGunDirection = new Vector3(0.002f, 0.0f, 1.0f);
    private Vector3 _rightGunDirection = new Vector3(-0.002f, 0.0f, 1.0f);
    
    private float _deltaX = 10.1f, _deltaY = 10.1f;
    private float _deltaTime = default;
    
    private void Start()
    {
        _deltaTime = _cooldown;
    }

    public void Update()
    {
        _deltaTime += Time.deltaTime;
    }

    public override bool GetPlayerInput(string buttonName)
    {
        return Input.GetButton(buttonName);
    }
    
    public override bool FireIfPossible()
    {
        if (_deltaTime >= _cooldown)
        {
            foreach (Transform point in _leftSourcePoints)
            {
                PoolsManager.GetPooledObject(PooledObjectType.BULLET, point.position, Quaternion.identity)
                    .GetLinkedComponent<Projectile>().Setup(_leftGunDirection);
            }
            
            foreach (Transform point in _rightSourcePoints)
            {
                PoolsManager.GetPooledObject(PooledObjectType.BULLET, point.position, Quaternion.identity)
                    .GetLinkedComponent<Projectile>().Setup(_rightGunDirection);
            }
            
            _deltaTime = 0.0f;

            return true;
        }
        else return false;
    }

    public override void SetCustomTargetPosition(Vector3 targetPosition)
    {
        throw new System.NotImplementedException();
    }

    public override void SetCustomTargetDirection(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }
}