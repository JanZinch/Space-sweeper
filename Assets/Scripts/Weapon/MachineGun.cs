using UnityEngine;

public class MachineGun : Weapon
{
    [SerializeField] private Transform _leftSourcePosition = default;
    [SerializeField] private Transform _rightSourcePosition = default;
    [SerializeField] private float _cooldown = 0.1f;

    private Vector3 _leftGunDirection = new Vector3(0.002f, 0.0f, 1.0f);
    private Vector3 _rightGunDirection = new Vector3(-0.002f, 0.0f, 1.0f);
    
    private float _deltaTime = default;

    private void Awake()
    {
        GetPlayerInput = Input.GetButton;
    }
    
    private void Start()
    {
        _deltaTime = _cooldown;
    }

    public void Update()
    {
        _deltaTime += Time.deltaTime;
    }

    public override void FireIfPossible()
    {
        if (_deltaTime >= _cooldown)
        {
            PoolsManager.GetObject(PooledObjectType.BULLET, _leftSourcePosition.position, Quaternion.identity)
                .GetLinkedComponent<Projectile>().Setup(_leftGunDirection);
            
            PoolsManager.GetObject(PooledObjectType.BULLET, _rightSourcePosition.position, Quaternion.identity)
                .GetLinkedComponent<Projectile>().Setup(_rightGunDirection);
            
            _deltaTime = 0.0f;
        }
    }
}