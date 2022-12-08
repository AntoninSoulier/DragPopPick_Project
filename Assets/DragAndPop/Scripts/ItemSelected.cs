using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSelected : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private string internalPath = "";
    public static string FullPathItem = "";
    private RectTransform rectTransform;
    public static List<MouseManager.ApplicationIcone> compatibleApp = new List<MouseManager.ApplicationIcone>();
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        FullPathItem = Application.dataPath + "\\"+internalPath;
        if (internalPath.Contains(".txt"))
        {
            compatibleApp.Add(MouseManager.ApplicationIcone.Word);
            compatibleApp.Add(MouseManager.ApplicationIcone.NodePad);
            compatibleApp.Add(MouseManager.ApplicationIcone.VsCode);
            compatibleApp.Add(MouseManager.ApplicationIcone.Excel);
        }
    }   

    public void OnBeginDrag(PointerEventData eventData)
    {
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
       // Debug.Log("OnPointerDown");
    }

    
}
