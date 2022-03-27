using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleTouuchBtnObj : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 _initialScale;
    [SerializeField] private float _touchedScale = 0.8f;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Transform>().localScale = _initialScale * _touchedScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Transform>().localScale = _initialScale;
    }

    private void Start()
    {
        _initialScale = GetComponent<Transform>().localScale;
    }
}
