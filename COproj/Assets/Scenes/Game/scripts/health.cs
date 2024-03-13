using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour
{
    public Slider healthslider;
    public int healthammount;
    public int maxHealth;
    public GameObject gameController;

     void Start()
    {
        healthslider.maxValue = maxHealth;
        healthslider.value = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("enemy"))
        {
           // int dmg = collision.transform.GetComponent<Zombie>().Damage;
            Damage(collision.transform.GetComponent<Zombie>().Damage);
            Destroy(collision.transform.gameObject);
        }
    }

    public void Damage(int DamageAmount) {
        healthammount = healthammount - DamageAmount;
        healthslider.value = healthammount;
        if (healthammount <=0)
        {
            highscore();
            SceneManager.LoadScene("GameOver");
        }
    }

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
