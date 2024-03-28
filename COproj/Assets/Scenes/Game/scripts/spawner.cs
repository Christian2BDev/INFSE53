using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    // hier word een lijst gemaakt, die in unity zelf word aangevuld met alle zombie types.
    public List<Enemy> enemies = new List<Enemy>();

    private GameObject[] spawnPoints;
    // zodra de game start word de spawn methode uitgevoerd
    void Start()
    {
        spawn();
    }

    // deze methodes find alle locaties van de spawnpoints, en plaats daar vervolgens een willekeurige zombie op en
    // verwijderd daarna de spawnpoint
    public void spawn() {

        spawnPoints = GameObject.FindGameObjectsWithTag("spawnpoint");
        foreach (var spawnpoint in spawnPoints)
        {
            Instantiate(enemies[Random.Range(0, enemies.Count)].prefab, spawnpoint.transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(spawnpoint);
        }
    }
}

[System.Serializable]
public class Enemy {
    public string name;
    public GameObject prefab;
 


}
