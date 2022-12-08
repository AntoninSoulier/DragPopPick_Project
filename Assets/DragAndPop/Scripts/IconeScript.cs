using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconeScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 baseAnchor;
    // Start is called before the first frame update
    void Start()
    {
        baseAnchor = GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseDirection.hovered = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseDirection.hovered = null;
    }
}
