using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable2B : MonoBehaviour
{
    private enum Hand {
        Right,
        Left
    }
    private GameObject oldHit;
    public GameObject seeObject;
    public GameObject selectedSlider;



    public LayerMask layer;
    public LayerMask backlayer;

    

    [SerializeField] private Hand hand = new Hand();


    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position += -transform.right / 10;
        }

        if (Input.GetKey(KeyCode.X))
        {
            transform.position += transform.right / 10;
        }

       

        //OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 999999999999999999f, layer))
        {
            GameObject g = hit.transform.gameObject;
                if (g.tag == "button2B")
                {
                    g.GetComponent<Renderer>().material.color = g.GetComponent<Button2B>().hover;


                    if (hand == Hand.Left) { LeftClick(g); }
                    if (hand == Hand.Right) { RightClick(g); }
                }
                
            


            if (g != oldHit) { UnFocus(); }

            oldHit = g;

            if (g.tag == "backdrop" || g.GetComponent<Audio2B>() !=null)
            {
                if (hand == Hand.Left) {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.LeftShift))
                    {
                        
                        setSlider();
                    }
                }
                if (hand == Hand.Right) {
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKey(KeyCode.RightShift))
                    {
                        setSlider();
                    }
                }

            }


            if (g.GetComponent<Audio2B>() != null)
            {
                selectedSlider = hit.transform.gameObject;
            }

            else if (g.layer == LayerMask.NameToLayer("sliderBack")&&  g.transform.parent.GetChild(1).GetComponent<Audio2B>() != null)
            {
                selectedSlider = g.transform.parent.GetChild(1).gameObject;
            }
        }
        else { UnFocus(); }

        aimPoint();
    }

    private void aimPoint()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 99999f))
        {
            GameObject g = hit.transform.gameObject;
            if (g.tag == "backdrop" || g.tag == "backgroun" || g.tag == "button2B")
            {
              
                seeObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }
    }

    private void UnFocus() {

        if (oldHit != null && oldHit.tag == "button2B")
        {
            oldHit.GetComponent<Renderer>().material.color = oldHit.GetComponent<Button2B>().normal;
        }
    }

    private void LeftClick(GameObject g) {

        
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.LeftShift))
        {
            g.GetComponent<Renderer>().material.color = g.GetComponent<Button2B>().pressed;
            g.GetComponent<Button2B>().pressedVoid();

        }
        else { g.GetComponent<Renderer>().material.color = g.GetComponent<Button2B>().hover; }
    }

    private void RightClick(GameObject g)  {
       
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKey(KeyCode.RightShift))
        {
            g.GetComponent<Renderer>().material.color = g.GetComponent<Button2B>().pressed;
            g.GetComponent<Button2B>().pressedVoid();

        }
        else { g.GetComponent<Renderer>().material.color = g.GetComponent<Button2B>().hover; }
    }


    private void setSlider() {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 99999f, backlayer))
        {
            if (selectedSlider != null)
            {
            selectedSlider.transform.position = new Vector3(hit.point.x, selectedSlider.transform.position.y, selectedSlider.transform.position.z);

            }
         }

        
    }
}
