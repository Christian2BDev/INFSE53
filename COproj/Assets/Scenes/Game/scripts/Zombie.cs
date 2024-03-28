using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    // zodra dit script op een object zit word het gezien als zombie.
    

    public string Name;
    public int Health;
    public int Damage;
    public int MovingSpeed;
    public int sightDistance;
    public int points;
    public Slider healtSlider;
    Rigidbody rb;

    // de levens slider boven de zombies hoofd word ingesteld op zijn levens
    void Start()
    {
        healtSlider.maxValue = Health;
        rb = GetComponent<Rigidbody>();
    }

    // deze zombie check continue of de auto in zijn weergave gebied is,
    // zo ja verplaats deze zombie zich langzaam naar de auto toe, ook kijkt hij naar de auto
    void Update()
    {
       

        if (Vector3.Distance(transform.position, GameObject.Find("car").transform.position) < sightDistance )
        {
         rb.position = Vector3.MoveTowards(transform.position, GameObject.Find("car").transform.position, MovingSpeed * Time.deltaTime);
            rb.transform.LookAt(GameObject.Find("car").transform);
        }
    }

    //zodra de zombie is geraakt door een geweer word deze methode uitgevoert
    // het haald de doorgeven variable van de zombies z'n levens af, zodra dit 0 word
    // word de methode die uitgevoerd
    public void DamageEnemy(int DamageAmount) {
        Health = Health - DamageAmount;
        healtSlider.value = Health;
        
        if (Health <= 0)
        {
            die();
        }
    }


    // deze methode zorgt ervoor dat het addpoints script word uitgevoerd en geeft de punten door wat deze zombie waard is
    // en vervolgens verwijderd de zombie
    void die() {
        GameObject.Find("GameController").GetComponent<score>().Addpoints(points);
        Destroy(gameObject);

    }
}
