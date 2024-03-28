using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable2B : MonoBehaviour
{

    // dit script is mij eigen zelf geschreven ui systeem.

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

    // ik heb het systeem erg modulair gemaakt zodat je het kan hergebruiken, dit heb ik gedaan door gebruik te maken van tags.
    // het werkt als volgt:
    /*
        er word continue een laser geschoten uit de locatie van je controller naar voren
        waar deze lazer land word een blokje geplaats om te laten weten waar je zit te aimen

        als het een knop is word de kleur van de knop veranderd , en word de bijbehorende hand functie aangeroepen, om te kijken of je klikt
        zodra je de knopt niet meer raakt word de kleur van de knop weer terug veranderd

        als het een slider is word voor de bijbehorende hand slider functie aangeroepen

       
     */
    void Update()
    {
        //dit is voor debugging
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position += -transform.right / 10;
        }

        if (Input.GetKey(KeyCode.X))
        {
            transform.position += transform.right / 10;
        }
        //tot hier
       

        //OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 999999999999999999f, layer))
        {
            GameObject g = hit.transform.gameObject;
                if (g.tag == "button2B")
                {
                g.GetComponent<Renderer>().material = g.GetComponent<Button2B>().hover;
                g.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                DynamicGI.UpdateEnvironment();
                

                if (hand == Hand.Left) { LeftClick(g); }
                    if (hand == Hand.Right) { RightClick(g); }
                }
                
            


            if (g != oldHit) { UnFocus(); }

            oldHit = g;
            //slider
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

    //deze methode zet een blokje waar de laser land
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
    //deze methode zorgt ervoor dat de geslecteerde knoppen weer terug veranderen naar hun standaard kleur
    private void UnFocus() {

        if (oldHit != null && oldHit.tag == "button2B")
        {
            oldHit.GetComponent<Renderer>().material=oldHit.GetComponent<Button2B>().normal;
            oldHit.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            DynamicGI.UpdateEnvironment();
        }
    }

    //zodra er op een knop word geklikt word de kleur nogmaals veranderd,
    // nu word er op de knop gekeken naar het button script, en word de functie pressed daarin uitgevoert
    private void LeftClick(GameObject g) {

        
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.LeftShift))
        {
            g.GetComponent<Renderer>().material=g.GetComponent<Button2B>().pressed;
            g.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            DynamicGI.UpdateEnvironment();
            g.GetComponent<Button2B>().pressedVoid();

        }
        else {
            g.GetComponent<Renderer>().material= g.GetComponent<Button2B>().hover;
            g.GetComponent<Renderer>().material.EnableKeyword("_EMISSION"); }
            DynamicGI.UpdateEnvironment();
    }
    //zelfde als hierboven maar dan voor linker hand, dit is dubbel om een bug op te lossen
    private void RightClick(GameObject g)  {
       
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKey(KeyCode.RightShift))
        {
            g.GetComponent<Renderer>().material= g.GetComponent<Button2B>().pressed;
            g.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            DynamicGI.UpdateEnvironment();
            g.GetComponent<Button2B>().pressedVoid();

        }
        else {
            g.GetComponent<Renderer>().material= g.GetComponent<Button2B>().hover;
            g.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            DynamicGI.UpdateEnvironment();
        }
    }

    // deze methode is special voor sliders, zodra je klikt op de slider word de sliderbol verplaats
    // naar je je de slider hebt geraakt. een script op de slider zelf weet wat hij dan met deze verandering moet doen
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
