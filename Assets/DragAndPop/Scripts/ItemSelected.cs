using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSelected : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private string internalPath = "";
    public static string FullPathItem = "";
    private RectTransform rectTransform;
    private List<MouseManager.ApplicationIcone> compatibleApp = new List<MouseManager.ApplicationIcone>();
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
        }
    }   

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        rectTransform.sizeDelta = new Vector2(50, 50);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta; 
        rectTransform.sizeDelta = new Vector2(45, 45);
    }
     
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
