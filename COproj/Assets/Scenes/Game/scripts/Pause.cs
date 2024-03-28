using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
   public GameObject pauseMenu;
    public GameObject aim1;
    public GameObject aim2;
    public bool paused;
    public GameObject passthrough;

    //Bug fix
    void Start()
    {
        Time.timeScale = 1;
    }

    // zodra er op de pauze knop word ge drukt word de pause functie aangeroepen
    // en nogmaals netzoals het pasthrought script zelf, word hier de passtrough getoggled. vooor in de game zelf
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start) || Input.GetKeyDown(KeyCode.B))
  
        {
            togglePause();
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))

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

    // deze methode pauzeerd de game. activeerd het pauze menu en zet de tijd stil
    public void togglePause()
    {
        paused = !paused;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        aim1.SetActive(!aim1.activeSelf);
        aim2.SetActive(!aim2.activeSelf);
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
        //SceneManager.LoadScene("Home");
       
    }

}
