using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public Slider healthslider;
    public int healthammount;
    public int maxHealth;
    

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
        healthslider.value = healthslider.value - DamageAmount;
    }
}
