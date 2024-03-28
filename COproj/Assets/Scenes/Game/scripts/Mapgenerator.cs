using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapgenerator : MonoBehaviour
{

    private int tempInt;
    public GameObject Map;
    public GameObject End;
    bool gen = false;
    int pos;
    GameObject[] aliveMap;
    [SerializeField] float inter;
    float interval;
    public Coroutine c;
    public Coroutine c2;

    // als de game start plaats deze methode x hoeveelheid onderdelen in de wereld gebasserd op het level dat je speelt
    // en op het eind plaats het de leger basis map

    // maar als het endles is start het een loop om continu een niewe onderdeel te plaatsen voor je en 1 oude weg te halen 
    void Awake()
    {
        tempInt = PlayerPrefs.GetInt("Level");

        interval = GameObject.Find("car").GetComponent<continue_movement>().speed;

        if (tempInt != 0)
        {
            for (int i = 0; i <= tempInt; i++)
            {
                if (i == tempInt)
                {
                    Instantiate(End, new Vector3(-100, -1, i * 100), Quaternion.Euler(0, 0, 0));
                }
                else
                {
                    Instantiate(Map, new Vector3(-100, -1, i * 100), Quaternion.Euler(0, 0, 0));
                }

            }
        }
        else {
            for (int i = 0; i <= 4; i++)
            {
                pos = i;
                Instantiate(Map, new Vector3(-100, -1, i * 100), Quaternion.Euler(0, 0, 0));
            }

         c2 = StartCoroutine(rere());
        }
        
    }

    //cooldown zorgt dat het niet gelijkt begint met weghalen van platforms, na 2 interfalls begint hij pas
    IEnumerator rere() {

        yield return new WaitForSeconds(inter * interval*2);
        c = StartCoroutine(re());
    }

    //dit is een loop dat zorgt dat elke interval de renew methode word aangeroepen
    IEnumerator re()
    {
        StopCoroutine(c2);
        Renew();
        yield return new WaitForSeconds(inter* interval);
        c = StartCoroutine(re());
    }

    // deze methode vind alle maps, en verwijderd vervolgens de oudste. en plaats weer een nieuwe vooraan.
    // en spawned ook nieuwe zombies daar op
    void Renew() {
        aliveMap = GameObject.FindGameObjectsWithTag("map_part");
        Destroy(aliveMap[0]);
        Instantiate(Map, new Vector3(-100, -1, pos * 100), Quaternion.Euler(0, 0, 0));
        GetComponent<spawner>().spawn();
        pos++;

    }



}
