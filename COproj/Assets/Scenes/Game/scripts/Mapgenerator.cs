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

    // Start is called before the first frame update
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

    IEnumerator rere() {

        yield return new WaitForSeconds(inter * interval*2);
        c = StartCoroutine(re());
    }


    IEnumerator re()
    {
        StopCoroutine(c2);
        Renew();
        yield return new WaitForSeconds(inter* interval);
        c = StartCoroutine(re());
    }

    void Renew() {
        Debug.Log("lol");
        aliveMap = GameObject.FindGameObjectsWithTag("map_part");
        Destroy(aliveMap[0]);
        Instantiate(Map, new Vector3(-100, -1, pos * 100), Quaternion.Euler(0, 0, 0));
        GetComponent<spawner>().spawn();
        pos++;

    }



}
