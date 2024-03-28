using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHelp : MonoBehaviour
{
    //script word niet meer gebruitkt.
    // maar zorgde ervoor dat een plaatje van de controls aan en uit werd gezet
    public GameObject controlImg;


    public void toggleImg() {
        controlImg.SetActive(!controlImg.active);
    }
}
