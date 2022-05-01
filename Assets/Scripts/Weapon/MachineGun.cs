using UnityEngine;

public class MachineGun : Weapon
{
    [SerializeField] private Transform _leftSourcePosition = default;
    [SerializeField] private Transform _rightSourcePosition = default;
    [SerializeField] private float _cooldown = 0.25f;


    public override void FireIfPossible()
    {
        
    }
}