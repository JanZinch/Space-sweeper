using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class AimingController : MonoBehaviour
{
    [SerializeField] private RectTransform _sight = null;

    /*private const string MouseXAxis = "Mouse X";
    private const string MouseYAxis = "Mouse Y";*/

    private const float DistanceToTarget = 10.0f;
    
    private Vector2 _cachedMousePosition = default;
    private Vector2 _sourceSightPosition = default;

    private static AimingController _instance = null;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _sourceSightPosition = new Vector2(Screen.width/2.0f, Screen.height/2.0f);
        
        
        Debug.Log("SP: " + _sourceSightPosition);
    }

    private void Update()
    {
        _cachedMousePosition = Input.mousePosition;
        _sight.position = _cachedMousePosition;
        
        Debug.Log("MP: " + _cachedMousePosition);
    }

    private static Vector3 ToVector3(Vector2 source)
    {
        Vector2 world = Camera.main.ScreenToWorldPoint(source);
        
        return new Vector3(world.x, world.y, DistanceToTarget);
    }

    public static Vector3 GetDirection()
    {
        /*Vector3 delta = _instance._cachedMousePosition - _instance._sourceSightPosition;

        delta.Normalize();
        
        delta.y *= 0.5f;
        delta.z = 1.0f;*/
        
        //Vector3 sum = _instance._sourceSightPosition + _instance._cachedMousePosition;
        //sum.z = 0.0f;
        
        //_instance.transform.LookAt(_cachedMousePosition);

        /*float delta = Vector3.Distance(_instance._cachedMousePosition, _instance._sourceSightPosition);

        Vector3 direction = Mathf.Pow(DistanceToTarget, 2) + Mathf.Pow(delta, 2)*/

        Vector3 delta = ToVector3(_instance._cachedMousePosition) - ToVector3(_instance._sourceSightPosition);

        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = ToVector3(_instance._sourceSightPosition);
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = ToVector3(_instance._cachedMousePosition);
        
        Vector3 direction = ((new Vector3(0.0f, 0.0f, DistanceToTarget) + delta)).normalized;

        return direction;
    }


}