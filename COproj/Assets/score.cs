using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class score : MonoBehaviour
{

    public TMP_Text scoreTxt;
    public TMP_Text highscoreTxt;

    public int points = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
        if (PlayerPrefs.HasKey("highscore"))
        {
        highscoreTxt.text = PlayerPrefs.GetInt("highscore").ToString();
        }
    }

    public void Addpoints(int p) {
        points= points + p;
        UpdateText();
    }

    void UpdateText() {
        scoreTxt.text = points.ToString();

    }
    
}
