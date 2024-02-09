using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public Slider healthslider;
    public int healthammount;
    public int maxHealth;
    public int DamageAmount;

     void Start()
    {
        healthslider.maxValue = maxHealth;
        healthslider.value = maxHealth;
    }


    public void Damage() {
        healthslider.value = healthslider.value - DamageAmount;
    }
}
