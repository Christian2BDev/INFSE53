using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHelp : MonoBehaviour
{

    public GameObject controlImg;


    public void toggleImg() {
        controlImg.SetActive(!controlImg.active);
    }
}
