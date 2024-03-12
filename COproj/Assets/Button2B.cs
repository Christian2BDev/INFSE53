using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class Button2B : MonoBehaviour
{
    public Color normal;
    public Color hover;
    public Color pressed;

    enum Functions {
        Play,
        Settings,
        Quit,
        BackToMain,
        MasterVolumeSlider,
        BackToHome,
        Resume,
        Help,
        HelpBack,
        Turtorial,
        Levels,
        Endless,
        BackToMode,
        PlayLvl
    }

    [SerializeField] Functions f = new Functions();




    void Start()
    {
        normal = GetComponent<Renderer>().material.color;

    }

    private void Update()
    {
    }

    public void pressedVoid() {
        switch (f)
        {
            case Functions.Play: Play(); break;
            case Functions.Settings: Settings(); break;
            case Functions.Quit: Quit(); break;
            case Functions.BackToMain: BackToMain(); break;
            case Functions.MasterVolumeSlider: MasterVolumeSlider(); break;
            case Functions.BackToHome: BackToHome(); break;
            case Functions.Resume: Resume(); break;
            case Functions.Help: Help(); break;
            case Functions.HelpBack: HelpBack(); break;
            case Functions.Turtorial: Turtorial(); break;
            case Functions.Levels: Levels(); break;
            case Functions.Endless: Endless(); break;
            case Functions.BackToMode: BackToMode(); break;
            case Functions.PlayLvl: PlayLvl(); break;
        }
    }

    //main menu
    private void Play()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(2).gameObject.SetActive(true);
    }
    private void Settings()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        Debug.Log("Settings");
    }
    private void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }



    //settings menu
    private void BackToMain()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(0).gameObject.SetActive(true);
    }

    private void MasterVolumeSlider() {

        GetComponent<Audio2B>().MasterVolumeSlider();
    }

    private void BackToHome() {
        SceneManager.LoadScene("Homescreen");
    }

    private void Resume(){
        GameObject pauseToggle = GameObject.Find("GameController");
        pauseToggle.GetComponent<Pause>().togglePause();
    }

    private void Help() {
        transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
        transform.parent.parent.GetChild(2).gameObject.SetActive(!transform.parent.parent.GetChild(2).gameObject.activeSelf);
    }

    private void HelpBack()
    {
        transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
        transform.parent.parent.GetChild(0).gameObject.SetActive(!transform.parent.parent.GetChild(2).gameObject.activeSelf);
    }

    private void Turtorial() {
        SceneManager.LoadScene("Turtorial");
        Debug.Log("Turtorial");
    }

    private void Levels()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(3).gameObject.SetActive(true);
    }

    private void Endless()
    {
        PlayerPrefs.SetInt("Level", 0);
       SceneManager.LoadScene("Game");
    }

    private void BackToMode()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(2).gameObject.SetActive(true);
    }

    private void PlayLvl() {
        PlayerPrefs.SetInt("Level", int.Parse(transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text));
        SceneManager.LoadScene("Game");
        //do shit
    }
}
