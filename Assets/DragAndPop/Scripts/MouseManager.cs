using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    private Vector3 startPosition;
    private IconeScript[] components;
    [SerializeField] private float epsAngle = 45;

    private void Start()
    {
        components = GameObject.FindObjectsOfType<IconeScript>();
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

            transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x,
                transform.localEulerAngles.y, 
                angle);
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
                comp.GetComponent<Image>().color = Color.blue;
            }
            else
            {
                comp.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
