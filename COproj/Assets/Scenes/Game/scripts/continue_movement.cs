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

    void FixedUpdate()
    {
        if (checkpoint <= rotatePoints.Count - 1)
        {
            MoveAndLook(rotatePoints[checkpoint]);
            
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
