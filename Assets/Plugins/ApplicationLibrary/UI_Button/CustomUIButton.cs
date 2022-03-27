using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.ApplicationLibrary.Application;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CustomUIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private CanvasGroup targetCanvasGroup;
    private float defaultScale = 0;
    private void Awake()
    {
        if (ApplicationUtils.GetGameInitializeState())
        {
            defaultScale = transform.localScale.x;
            if (GetComponent<CanvasGroup>() == null) gameObject.AddComponent<CanvasGroup>();
            targetCanvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(defaultScale * 0.92f, 0.1f);
        targetCanvasGroup.DOFade(0.9f, 0.1f);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(defaultScale, 0.1f);
        targetCanvasGroup.DOFade(1, 0.1f);
    }
}
