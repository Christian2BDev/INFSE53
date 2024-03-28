using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class score : MonoBehaviour
{

    public TMP_Text scoreTxt;
    public TMP_Text highscoreTxt;

    public int points = 0;

    // als het geen endless modes is worden de texen uit gezet, omdat in levels je geen score hoord te hebben
    // de highscore text gelijk gezet aan je highscore, als je die hebt
    void Start()
    {
        if (PlayerPrefs.GetInt("Level") !=0)
        {
            scoreTxt.transform.parent.gameObject.SetActive(false);
        }


        UpdateText();
        if (PlayerPrefs.HasKey("highscore"))
        {
        highscoreTxt.text = PlayerPrefs.GetInt("highscore").ToString();
        }
    }


    // deze methode voegt punten toen an je score en triggered dan de updateText functie
    public void Addpoints(int p) {
        points= points + p;
        UpdateText();
    }

    // deze functie update's de text zodat het weer de goede score laat zien
    void UpdateText() {
        scoreTxt.text = points.ToString();

    }
    
}
