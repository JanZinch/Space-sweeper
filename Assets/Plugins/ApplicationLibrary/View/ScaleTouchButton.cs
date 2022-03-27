using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Alexplay.OilRush.Library.View
{
    public class ScaleTouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Vector2 _initialScale;
        [SerializeField] private float _touchedScale = 0.8f;

        [SerializeField] private bool _touchOnlyInteractible = false;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_touchOnlyInteractible==true)
            {
                if (gameObject.GetComponent<Button>()!= null)
                    if (gameObject.GetComponent<Button>().interactable)
                        GetComponent<RectTransform>().localScale = _initialScale * _touchedScale;
            }
            else
                GetComponent<RectTransform>().localScale = _initialScale * _touchedScale;
        }

        public void OnPointerUp(PointerEventData eventData) => GetComponent<RectTransform>().localScale = _initialScale;

        private void Start()
        {
            _initialScale = GetComponent<RectTransform>().localScale;
        }
    }
}