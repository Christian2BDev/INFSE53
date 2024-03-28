using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class Button2B : MonoBehaviour
{

    /*zodra dit script op een object word gedaan word het gezien als knop.
      je kunt dingen aanpassen zoals de kleur van de knop, de actie, ect.

    */
   public Material normal;
   public Material hover;
   public Material pressed; 

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
        PlayLvl,
        EndPopup
    }

    [SerializeField] Functions f = new Functions();




    void Awake()
    {
        normal = GetComponent<Renderer>().material;
        hover = Resources.Load("hovermat") as Material;
        pressed = Resources.Load("clickmat") as Material;


    }

    private void Update()
    {
    }

    //als er op de knop word gedrukt word deze functie geroepen van een ander script.
    // het kijkt nu naar welke functie je hebt toegedient aan dze knop en voert de bijbehordende functie uit
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
            case Functions.EndPopup: EndPopup(); break;
        }
    }

    //zorgt dat je het volgende menu tezien krijgt (turtorial, levels, endles) menu
    private void Play()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(2).gameObject.SetActive(true);
    }
    // zorgt ervoor dat je het settings menu tezien krijgt
    private void Settings()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        Debug.Log("Settings");
    }

    //sluit de applicatie af
    private void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }



    //een methode om weer terug te gaan naar het eerste menu
    private void BackToMain()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(0).gameObject.SetActive(true);
    }
    // een methode specefiek voor audio sliders is. het roept de audio functie aan op de slider
    private void MasterVolumeSlider() {

        GetComponent<Audio2B>().MasterVolumeSlider();
    }

    // laad het hoofmenu scene
    private void BackToHome() {
        SceneManager.LoadScene("Homescreen");
    }

    // haalt het object GameController op, die alle belangrijke zaken regelt, en voert daarop het toggle pause functie uit.
    // dit zorgt ervoor dat de game weer van pause af gaat
    private void Resume(){
        GameObject pauseToggle = GameObject.Find("GameController");
        pauseToggle.GetComponent<Pause>().togglePause();
    }

    // laat het controls plaatje zien
    private void Help() {
        transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
        transform.parent.parent.GetChild(2).gameObject.SetActive(!transform.parent.parent.GetChild(2).gameObject.activeSelf);
    }

    // verbergt het controls plaatje
    private void HelpBack()
    {
        transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
        transform.parent.parent.GetChild(0).gameObject.SetActive(!transform.parent.parent.GetChild(2).gameObject.activeSelf);
    }

    // laad de turtorial wereld
    private void Turtorial() {
        SceneManager.LoadScene("Tutorial");
        Debug.Log("Turtorial");
    }

    // laat het levels menu zien
    private void Levels()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(3).gameObject.SetActive(true);
    }

    // laad de game, met een Level waarde van 0 wat endless modes instelt
    private void Endless()
    {
        PlayerPrefs.SetInt("Level", 0);
       SceneManager.LoadScene("Game");
    }

    // gaat terug naar de mode selecteer menu
    private void BackToMode()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.GetChild(2).gameObject.SetActive(true);
    }

    // laad de game , met een level waarde van welke knop je hebt opgeklikt (van 1 tot 10). dit start het level
    private void PlayLvl() {
        PlayerPrefs.SetInt("Level", int.Parse(transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text));
        SceneManager.LoadScene("Game");
        //do shit
    }

    // herstart de game
    private void EndPopup() {
        SceneManager.LoadScene("Game");
    }
}
