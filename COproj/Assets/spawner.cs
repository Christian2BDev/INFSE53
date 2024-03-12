using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public List<Enemy> enemies = new List<Enemy>();

    private GameObject[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    public void spawn() {

        spawnPoints = GameObject.FindGameObjectsWithTag("spawnpoint");
        foreach (var spawnpoint in spawnPoints)
        {
            Instantiate(enemies[Random.Range(0, enemies.Count - 1)].prefab, spawnpoint.transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(spawnpoint);
        }
    }
}

[System.Serializable]
public class Enemy {
    public string name;
    public GameObject prefab;
 


}
