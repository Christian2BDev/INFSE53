using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public string Name;
    public int Health;
    public int Damage;
    public int MovingSpeed;
    public int sightDistance;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Vector3.Distance(transform.position, GameObject.Find("car").transform.position) < sightDistance )
        {
         rb.position = Vector3.MoveTowards(transform.position, GameObject.Find("car").transform.position, MovingSpeed * Time.deltaTime);
            rb.transform.LookAt(GameObject.Find("car").transform);
        }
    }

    public void DamageEnemy(int DamageAmount) {
        Health = Health - DamageAmount;
        if (Health <= 0)
        {
            Destroy(this);
        }
    }
}
