using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class AimingController : MonoBehaviour
{
    [SerializeField] private RectTransform _sight = null;

    private const string MouseXAxis = "Mouse X";
    private const string MouseYAxis = "Mouse Y";

    private Vector2 _cachedMousePosition = default;

    private Vector2 _sourceSightPosition = default;

    private void Start()
    {
        _sourceSightPosition = _sight.position;
    }

    private void Update()
    {
        _cachedMousePosition = Input.mousePosition;
        _sight.position = _cachedMousePosition;
    }

    private Vector3 GetDirection()
    {
        Vector3 sum = _sourceSightPosition + _cachedMousePosition;
        sum.z = 0.0f;
        
        return sum + Vector3.forward;
    }


}