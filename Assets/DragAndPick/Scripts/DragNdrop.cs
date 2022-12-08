using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragNdrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    public static bool isDragged = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = true;
        rectTransform.sizeDelta = new Vector2(45, 45);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.sizeDelta = new Vector2(50, 50);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta; 
        rectTransform.sizeDelta = new Vector2(45, 45);
    }
     
    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
