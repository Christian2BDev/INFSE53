using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio2B : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerGroup mixerGroup;


     void Start()
    {
        
        if (PlayerPrefs.HasKey(mixerGroup.name))
        {
        transform.position = new Vector3(procentageToTranslate(PlayerPrefs.GetFloat(mixerGroup.name)).x, transform.position.y, transform.position.z);
        }
    }
    // Update is called once per frame
    void Update()
    {
        MasterVolumeSlider();
    }

    public void MasterVolumeSlider()
    {
        PlayerPrefs.SetFloat(mixerGroup.name, translateToProcentage(transform));
        mixer.SetFloat(mixerGroup.name, Mathf.Log10( translateToProcentage(transform)) * 20);
       
    }

    private float translateToProcentage(Transform currentPos)
    {
        /*
         length slider is 3
         slider begint op -1,5

        transform.postion.x = 1 -> 1.5 + 1.5/3
        transform.postion.x = 0 -> 
         */

        float sliderStart = -1.5f;
        float procentage;

        procentage = (-sliderStart + transform.position.x) / 3;
        return procentage;
    }

    private Vector3 procentageToTranslate(float procentage) {

        /*
         y = (1.5 + x) / 3
        3y = 1.5 + x
        x = 3y - 1.5
         x = 3*procentage - 1.5
         */
        Vector3 pos = new Vector3((3*procentage)-1.5f,0,0);
        return pos;
    }
}
