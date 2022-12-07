using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MouseDirection : MonoBehaviour
{
    private Vector3 startPosition;
    private IconeScript[] components;
    private List<IconeScript> selectedIcones = new List<IconeScript>();
    [SerializeField] private float epsAngle = 45;

    private void Start()
    {
        components = GameObject.FindObjectsOfType<IconeScript>();
        print(components.Length);
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
            
            float angle = Mathf.Atan2 (mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
            if (angle<0) angle += 360;
             
            //Debug.Log (angle);
            FindAllIcones(startPosition,angle);

            transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x,transform.localEulerAngles.y, angle);
        }
    }

    public void FindAllIcones(Vector3 startPosition, float angleMouse)
    {
        foreach (var comp in components)
        {

            Vector3 iconeDelta = comp.transform.position - startPosition;
            if (iconeDelta.sqrMagnitude < 0.1f)
            {
                return; // don't do tiny rotations.
            }
            float angleIcone = Mathf.Atan2 (iconeDelta.y, iconeDelta.x) * Mathf.Rad2Deg;
            if (angleIcone<0) angleIcone += 360;
            print(angleIcone);
            if ((Math.Abs(angleMouse - angleIcone))%360 < epsAngle)
            {
                comp.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
                Debug.Log(comp.transform.position);
                if (!selectedIcones.Contains(comp))
                {
                    selectedIcones.Add(comp);
                }
                print(selectedIcones.Count);
                
                Vector3 mousepos = Input.mousePosition;
                Display(selectedIcones,mousepos);
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
