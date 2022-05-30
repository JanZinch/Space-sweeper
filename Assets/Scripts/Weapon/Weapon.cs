using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract bool GetPlayerInput(string buttonName);
    
    public abstract bool FireIfPossible();

    public abstract void SetCustomTargetPosition(Vector3 targetPosition);

    public abstract void SetCustomTargetDirection(Vector3 direction);
}
