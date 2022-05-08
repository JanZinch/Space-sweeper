using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract bool GetPlayerInput(string buttonName);
    
    public abstract void FireIfPossible();
}
