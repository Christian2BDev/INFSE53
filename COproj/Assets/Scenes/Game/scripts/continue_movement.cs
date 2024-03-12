using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continue_movement : MonoBehaviour
{

    public float speed;
    public float rotation_speed;

    public int checkpoint = 0;
    public int moving_state;

    public float y_ofset;
    public List<Vector3> rotatePoints = new List<Vector3>();

    private void Start()
    {
        GameObject[] terrains = GameObject.FindGameObjectsWithTag("map_part");
        foreach (var terrain in terrains)
        {
            rotatePoints.Add(new Vector3(terrain.transform.position.x +
                100, terrain.transform.position.y, terrain.transform.position.z));
        }
    }

    void FixedUpdate()
    {
       

        if (checkpoint <= rotatePoints.Count - 1 && PlayerPrefs.GetInt("Level")!=0)
        {
            if (rotatePoints[checkpoint] == new Vector3(0, -1, 0))
            {
                checkpoint++;
            }
            else {
                MoveAndLook(rotatePoints[checkpoint]);
            }


        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;

        }
    }

    void MoveAndLook(Vector3 point)
    {

        Vector3 real_point = new Vector3(point.x, point.y + y_ofset, point.z);
        if (Vector3.Distance(real_point, transform.position) <= 2) checkpoint++;

        Quaternion end = Quaternion.LookRotation(real_point - transform.position);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, end, rotation_speed *Time.deltaTime);
      transform.position += transform.forward * speed * Time.deltaTime;
    }

   
}
