using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseDirection : MonoBehaviour
{
    private Vector3 startPosition;
    private IconeScript[] components;
    private Slot[] slots;
    private Dictionary<IconeScript, Vector3> selectedIcones = new Dictionary<IconeScript, Vector3>();
    private bool isSelected = false;
    private Transform containerSlot;
    [SerializeField] private float epsAngle = 45;
    [SerializeField] private Vector3 offsetSlots = Vector3.zero;
    [SerializeField] private float minMapX = 0;
    [SerializeField] private float maxMapX = 100;
    [SerializeField] private float minMapY = 0;
    [SerializeField] private float maxMapY = 100;
    public static IconeScript hovered;


    int dfx = 8;
    int dfy = 8;
    private IconeScript[,] array1;
    IconeScript[,] array2;

    private void Awake()
    {
        components = FindObjectsOfType<IconeScript>();
        slots = FindObjectsOfType<Slot>();
        if (!(slots.Length == 0))
        {
            containerSlot = slots[0].transform.parent;
        }
    }

    private void Start()
    {
        //print("Slots: "+slots.Length);
        //tab();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseDelta = Input.mousePosition - startPosition;

            if (mouseDelta.sqrMagnitude < 0.1f)
            {
                return; // don't do tiny rotations.
            }

            float angleIcone = getAngle(mouseDelta);

            //Debug.Log (angle);
            FindAllIcones(startPosition, angleIcone);

            transform.localEulerAngles =
                new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angleIcone);

        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
            foreach (IconeScript comp in selectedIcones.Keys)
            {
                comp.transform.position = selectedIcones[comp];
            }

            selectedIcones.Clear();
            if (hovered != null)
            {
                if (DragNdrop.isDragged)
                {
                    
                    DragNdrop item = FindObjectOfType<DragNdrop>();
                    int x = 0;
                    int y = 0;
                    if (hovered.baseAnchor.x<0)
                    {
                        x = 50;
                    }
                    else
                    {
                        x = -50;
                    }
                    if (hovered.baseAnchor.y<0)
                    {
                        y = 50;
                    }
                    else
                    {
                        y = -50;
                    }
                    item.transform.position = hovered.transform.position+ new Vector3(x,y);
                }
                hovered.Execute("");

            }

            DragNdrop.isDragged = false;
        }

    }

    private float getAngle(Vector3 mouseDelta)
    {
        float angle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        return angle;
    }

    public void FindAllIcones(Vector3 startPosition, float angleMouse)
    {
        foreach (var comp in components)
        {
            Vector3 compPos = comp.transform.position;
            if (selectedIcones.ContainsKey(comp))
            {
                compPos = selectedIcones[comp];
            }

            Vector3 iconeDelta = compPos - startPosition;

            if (iconeDelta.sqrMagnitude < 0.05f)
            {
                return; // don't do tiny rotations.
            }

            float angleIcone = getAngle(iconeDelta);

            if ((Math.Abs(angleMouse - angleIcone)) % 360 < (int) epsAngle)
            {
                isSelected = true;
                //moveSlots();
                if (!selectedIcones.ContainsKey(comp))
                {
                    selectedIcones.Add(comp, comp.transform.position);
                    Vector3 mousepos = Input.mousePosition;
                    foreach (var c in selectedIcones.Keys)
                    {
                        Display(comp, mousepos);
                    }
                    
                }
            }
            else
            {
                //print("deselect");
                if (selectedIcones.ContainsKey(comp))
                {
                    comp.transform.position = selectedIcones[comp];
                    selectedIcones.Remove(comp);
                }

                if (selectedIcones.Count == 0)
                {
                    isSelected = false;
                }

            }
        }
    }

    public void Display(IconeScript icone, Vector3 mousepos)
    {
        /*
        for (int i = 0; i < selectedIcones.Count; i++)
        {
            Slot slot = slots[i];
            //print(slot.GameObject().name);
            float x = slot.GetComponent<RectTransform>().position.x;
            float y = slot.GetComponent<RectTransform>().position.y;
            //print("x: " + x + " y: " + y);
            Vector3 test = new Vector3();
            test = slot.GetComponent<RectTransform>().anchoredPosition;
            selectedIcones[i].transform.position = slot.transform.position - test;
        }
        */
//        print(icone.name);
        GameObject item = FindObjectOfType<DragNdrop>().gameObject;
        Vector2 itemPos = item.transform.position;
        //float x = Remap(icone.GetComponent<RectTransform>().anchoredPosition.x, -Screen.width/2, Screen.width/2, minMapX, maxMapX);
        //float y = Remap(icone.GetComponent<RectTransform>().anchoredPosition.y, -Screen.height/2, Screen.height/2, minMapY, maxMapY);

        Vector2 iconPos = selectedIcones[icone];
        
        float x = iconPos.x;
        float y = iconPos.y;
        
        if (iconPos.x < 0)
        {
            x = offsetSlots.x;
        }
        else
        {
            x = offsetSlots.x;
        }
        
        if (iconPos.y < 0)
        {
            y = offsetSlots.y;
        }
        else
        {
            y = offsetSlots.y;
        }

        var test = iconPos-itemPos;
        
        x += test.x/2f;
        y += test.y/2f;

        //print(icone.GetComponent<RectTransform>().anchoredPosition);
        icone.transform.position =  itemPos+new Vector2(x,y); //+posItem.x/2,y+posItem.y/2);

    }

}