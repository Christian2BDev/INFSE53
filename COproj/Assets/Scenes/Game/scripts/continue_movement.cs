using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class continue_movement : MonoBehaviour
{

    public float speed;
    public float rotation_speed;

    public int checkpoint = 0;
    public int moving_state;

    public float y_ofset;
    public List<Vector3> rotatePoints = new List<Vector3>();
    public GameObject screen;
    public TMP_Text screenTxt;
    public TMP_Text screenButtonTxt;


    // als de game start haalt hij alle wereld onderdelen op. zo weet hij wwaar hij naartoe moet rijden.
    // hij slaat ze op in een lijst
    private void Start()
    {
        GameObject[] terrains = GameObject.FindGameObjectsWithTag("map_part");
        foreach (var terrain in terrains)
        {
            rotatePoints.Add(new Vector3(terrain.transform.position.x +
                100, terrain.transform.position.y, terrain.transform.position.z));
        }
    }



    // als het een level is gaat hij de lijst met coordinaten af, en rijd hij er naar toe met de methode MoveAndLook
    // zodra de auto op het laast punt in de lijst is aangekomen laast hij een menu zien met opties gebaseerd op welk level je hebt gehaald
    // als het endless is gaat hij gewoon dom vooruit
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("Level") != 0)
        {
            if (checkpoint <= rotatePoints.Count - 1)
            {
                if (rotatePoints[checkpoint] == new Vector3(0, -1, 0))
                {
                    checkpoint++;
                }
                else
                {
                    MoveAndLook(rotatePoints[checkpoint]);
                }


            }
        }
        else if(PlayerPrefs.GetInt("Level") == 0)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

        }

        if (Vector3.Distance(transform.position, rotatePoints[rotatePoints.Count - 1]) <= 2)
        {
            
            if (PlayerPrefs.GetInt("Level") != 10)
            {
                screenTxt.text = "you did it!";
                screenButtonTxt.text = "next level!";
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            }
            else if (PlayerPrefs.GetInt("Level") == 10)
            {
                screenTxt.text = "you did it! you completed the game!";
                screenButtonTxt.text = "play endless mode!";
                PlayerPrefs.SetInt("Level", 0);
            }
            Time.timeScale = 0;
            screen.SetActive(true);
        }


    }
    // deze methode zorgt ervoor dat de auto naar het volgende punt draait en er naartoe gaat bewegen.
    // vorvolgens als hij het punt berijkt heeft gaat hij naar het volgende punt in de lijst
    void MoveAndLook(Vector3 point)
    {

        Vector3 real_point = new Vector3(point.x, point.y + y_ofset, point.z);
        if (Vector3.Distance(real_point, transform.position) <= 2) checkpoint++;

        Quaternion end = Quaternion.LookRotation(real_point - transform.position);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, end, rotation_speed *Time.deltaTime);
      transform.position += transform.forward * speed * Time.deltaTime;
    }

   
}
