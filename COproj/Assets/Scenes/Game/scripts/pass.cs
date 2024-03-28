using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pass : MonoBehaviour
{

    public GameObject passthrough;
   
    // de methode zorgt ervoor zodra je op de 2 knop op je controller klikt, de passthrough aan en uit word gezet. doormiddel van de opactiy te veranderen
    void FixedUpdate()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.Space))

        {
            if (passthrough.GetComponent<OVRPassthroughLayer>().textureOpacity == 1)
            {
                passthrough.GetComponent<OVRPassthroughLayer>().textureOpacity = 0;
            }
            else if (passthrough.GetComponent<OVRPassthroughLayer>().textureOpacity == 0)
            {
                passthrough.GetComponent<OVRPassthroughLayer>().textureOpacity = 1;
            }
        }
    }
}
