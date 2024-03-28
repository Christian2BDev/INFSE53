using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class health : MonoBehaviour
{

    //dit is het levels script van de auto. dit script zorgt ervoor dat je dood kan gaan


    public Slider healthslider;
    public int healthammount;
    public int maxHealth;
    public GameObject gameController;
    public GameObject screen;
    public TMP_Text screenTxt;
    public TMP_Text screenButtonTxt;

    // zet de health slider naar de maximaal ingestelde hoeveelheid
    void Start()
    {
        healthslider.maxValue = maxHealth;
        healthslider.value = maxHealth;
    }

    //elke keer als een object de auto raakt. kijkt hij of het een zombie is
    // als dat waar is pakt hij van de zombie zn damage ammount variable en voert daarmee de damage functie uit
    // daarna verwijderd hij de zombie. 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("enemy"))
        {
           // int dmg = collision.transform.GetComponent<Zombie>().Damage;
            Damage(collision.transform.GetComponent<Zombie>().Damage);
            Destroy(collision.transform.gameObject);
        }
    }

    // deze methode haalt het doorgegeven damage variable van de health af
    // als je health 0 is geworden stopt hij de game en laat hij het menu zien om opnieuwe te proberen
    public void Damage(int DamageAmount) {
        healthammount = healthammount - DamageAmount;
        healthslider.value = healthammount;
        if (healthammount <=0)
        {
            highscore();
            //SceneManager.LoadScene("GameOver");
            Time.timeScale = 0;
            screenTxt.text = "look like you died!";
            screenButtonTxt.text = "try again!";
            screen.SetActive(true);
        }
    }
    // deze moethode kijkt of de score die je nu hebt gehaald hoger is dan je high score
    // als dit waar is slaat hij je gehaalde score op al highscore
    void highscore() {
        if (PlayerPrefs.HasKey("highscore")) 
        {
            if (gameController.GetComponent<score>().points > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", gameController.GetComponent<score>().points);
            }
        }
        else
        {
            PlayerPrefs.SetInt("highscore", gameController.GetComponent<score>().points);
        }
    }
}
