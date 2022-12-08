using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconeScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public MouseManager.ApplicationIcone appType;
    
    private string applicationPath;
    // Start is called before the first frame update

    public Vector2 baseAnchor;
    // Start is called before the first frame update
    void Start()
    {
        switch (appType)
        {
            case MouseManager.ApplicationIcone.Excel:
                applicationPath = ListPathApplication.excelPath;
                break;
            case MouseManager.ApplicationIcone.Word:
                applicationPath = ListPathApplication.wordPath;
                break;
            case MouseManager.ApplicationIcone.NodePad:
                applicationPath = ListPathApplication.nodepadPath;
                break;
            case MouseManager.ApplicationIcone.VsCode:
                applicationPath = ListPathApplication.vscodePath;
                break;
            case MouseManager.ApplicationIcone.Steam:
                applicationPath = ListPathApplication.steamPath;
                break;
            case MouseManager.ApplicationIcone.Calculatrice:
                applicationPath = ListPathApplication.teamsPath;
                break;
        }
        baseAnchor = GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute(string PathToEx)
    {
        Process process = Process.Start(applicationPath, PathToEx);
        process.WaitForExit();
        process.Close();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseDirection.hovered = this;
        MouseManager.hovered = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseDirection.hovered = null;
        MouseManager.hovered = null;
    }
}
