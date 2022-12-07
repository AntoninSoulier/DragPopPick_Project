using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MouseDirection : MonoBehaviour
{
    private Vector3 startPosition;
    private IconeScript[] components;
    private Slot[] slots;
    private Dictionary<IconeScript,Vector3> selectedIcones = new Dictionary<IconeScript,Vector3>();
    private bool isSelected = false;
    private Transform containerSlot;
    [SerializeField] private float epsAngle = 45;
    [SerializeField] private Vector3 offsetSlots = Vector3.zero;
    [SerializeField] private float minMap = 0;
    [SerializeField] private float maxMap = 100;

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
        
    }
    
    void Update() {
         
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
            FindAllIcones(startPosition,angleIcone);

            transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x,transform.localEulerAngles.y, angleIcone);
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
            foreach (IconeScript comp in selectedIcones.Keys)
            {
                comp.transform.position = selectedIcones[comp];
            }
            selectedIcones.Clear();
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
            
            if ((Math.Abs(angleMouse - angleIcone))%360 < (int) epsAngle)
            {
                isSelected = true;
                //moveSlots();
                comp.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
                if (!selectedIcones.ContainsKey(comp))
                {
                    selectedIcones.Add(comp,comp.transform.position);
                    Vector3 mousepos = Input.mousePosition;
                    Display(selectedIcones.Keys.ToList(),mousepos);
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

    public void Display(List<IconeScript> selectedIcones, Vector3 mousepos)
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
        for (int i = 0; i < selectedIcones.Count; i++)
        {
            Vector3 posItem = FindObjectOfType<DragNdrop>().transform.position;
            float x = Remap(selectedIcones[i].transform.position.x, 0, Screen.width, minMap, maxMap);
            float y = Remap(selectedIcones[i].transform.position.y, 0, Screen.height, minMap, maxMap);
            print(x +" "+ y);
            selectedIcones[i].transform.position = new Vector2(x+posItem.x,y+posItem.y);
        }
    }
    /*
    public void moveSlots()
    {
        if (containerSlot != null)
        {
            Vector3 posItem = FindObjectOfType<DragNdrop>().transform.position;
            containerSlot.transform.position = posItem + offsetSlots;
        }
    }
    */
    
    float Remap(float source, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
    {
        return targetFrom + (source-sourceFrom)*(targetTo-targetFrom)/(sourceTo-sourceFrom);
    }
}