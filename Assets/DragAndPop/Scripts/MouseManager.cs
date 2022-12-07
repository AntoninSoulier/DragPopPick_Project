using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    private Vector3 startPosition;
    private IconeScript[] components;
    private List<IconeScript> selectedIcones = new List<IconeScript>();
    [SerializeField] private float epsAngle = 45;
    private IconeScript selectedIcon;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color notCompatible = Color.grey;


    public enum ApplicationIcone
    {
        Excel,
        VisualStudioCode,
        BlocNote,
        Word
    }
    
    private void Start()
    {
        components = FindObjectsOfType<IconeScript>();
    }

    void Update()
    {

        firstLeftClick();
        if (Input.GetMouseButton(0))
        {
            leftClickLong();
            colorThem();
        }
        if ((Input.GetMouseButtonUp(0)))
        {
            notLeftClick();
            colorThem();
        }
    }

    private void leftClickLong()
    {

        Vector3 mouseDelta = Input.mousePosition - startPosition;
         
        if (mouseDelta.sqrMagnitude < 0.1f)
        {
            return; // don't do tiny rotations.
        }

        float angle = getAngle(mouseDelta);
        FindAllIcones(startPosition,angle);
        transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x,
            transform.localEulerAngles.y, 
            angle);
    }

    private void notLeftClick()
    {
        selectedIcon = null;
        foreach (var comp in components)
        {
            comp.GetComponent<Image>().color = Color.white;
        }
    }
    
    private void firstLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
    }

    private float getAngle(Vector3 mouseDelta)
    {
        float angle = Mathf.Atan2 (mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
        if (angle<0) angle += 360;
        
        return angle;
    }

    
    public void FindAllIcones(Vector3 startPosition, float angleMouse)
    {
        foreach (var comp in components)
        {

            Vector3 iconeDelta = comp.transform.position - startPosition;
            if (iconeDelta.sqrMagnitude < 0.05f)
            {
                return; // don't do tiny rotations.
            }
            float angleIcone = Mathf.Atan2 (iconeDelta.y, iconeDelta.x) * Mathf.Rad2Deg;
            if (angleIcone<0) angleIcone += 360;
            print(angleIcone);
            if ((Math.Abs(angleMouse - angleIcone))%360 < epsAngle)
            {
                selectedIcon = comp;
                return;
            }
        }

        selectedIcon = null;
    }

    public void colorThem()
    {
        foreach (var comp in components)
        {
            if (comp == null)
            {
                return;
            }
            if(selectedIcon == comp)
            {
                comp.GetComponent<Image>().color = selectedColor;
            }
            else
            {
                //comp.transform.position = new Vector3(96, 422, 0);
                comp.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                selectedIcones.Remove(comp);
            }
        }
    }

    public void Display(List<IconeScript> selectedIcones, Vector3 mousepos)
    {
        int offset = 50;
        
        foreach (var icone in selectedIcones)
        {
            icone.transform.position = new Vector3(mousepos.x - offset, mousepos.y, 0);
            offset += 50;
        }
    }
}

