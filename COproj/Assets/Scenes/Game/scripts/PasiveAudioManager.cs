using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PasiveAudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerGroup[] mixerGroups;
    // Start is called before the first frame update
    void Start()
    {
        mixerGroups = mixer.FindMatchingGroups("");

        foreach (var mix in mixerGroups)
        {
            if (PlayerPrefs.HasKey(mix.name))
            {
            mixer.SetFloat(mix.name, Mathf.Log10(PlayerPrefs.GetFloat(mix.name)) * 20);
            }

          
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
