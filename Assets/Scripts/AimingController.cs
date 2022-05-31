using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class AimingController : MonoBehaviour
{
    [SerializeField] private RectTransform _sight = null;
    [SerializeField] private Camera _camera = null;
    /*private const string MouseXAxis = "Mouse X";
    private const string MouseYAxis = "Mouse Y";*/

    private const float DistanceToTarget = 50.0f;
    
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
        _sight.position = Input.mousePosition;
    }

    private static Vector3 ToVector3(Vector2 source)
    {
        var world = _instance._camera.ScreenToWorldPoint(new Vector3(source.x, source.y, DistanceToTarget));

        return world;
    }
    
    private static Vector3 Center(Vector2 source)
    {
        var world = _instance._camera.ScreenToWorldPoint(source);

        //world.y -= 2.0f;
        
        return world;
    }

    public static Vector3 GetDirection()
    {
        var v1 = ToVector3(Input.mousePosition);
        var v2 = Center(_instance._sourceSightPosition);
        Vector3 delta = v1 - v2;
        
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = v2;
        
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = v2;
        
        Vector3 direction = ((new Vector3(0.0f, 0.0f, DistanceToTarget) + delta)).normalized;

        Debug.Log("Dir:" + direction);
        
        
        return direction;
    }


}