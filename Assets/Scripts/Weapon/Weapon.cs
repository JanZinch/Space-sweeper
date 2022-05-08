using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Func<string, bool> GetPlayerInput = null;
    
    public abstract void FireIfPossible();
}
