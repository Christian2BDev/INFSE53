using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continues_movement : MonoBehaviour
{
    public float speed;
    public float rotation_speed;

    
    public bool rotating = false;
    // checkpoint change detection
    // done rotating bool
    public int checkpoint = 4;
    public int moving_state;
    /*
     1 -> forward
     2 -> turn 90 degrees
     */
    public float y_ofset;
    public List<Vector3> rotatePoints = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        /*
         checkpoint

        1 -> 90* 
        2 -> 180*
        3 -> 90*
        4 -> 0*
         */
    }

    // Update is called once per frame
    void Update()
    {
        if (checkpoint -4 <= rotatePoints.Count -1 )
        {
            move(rotatePoints[checkpoint -4]);
         
                if(checkpoint % 4 == 1) rotate(new Vector3(0, 90, 0));
                if (checkpoint % 4 == 2) rotate(new Vector3(0, 180, 0));
                if (checkpoint % 4 == 3) rotate(new Vector3(0, 90, 0));
                if (checkpoint % 4 == 0) rotate(new Vector3(0, 0, 0));
            
           
        }
        
    }

    void move(Vector3 point)
    {
        Vector3 real_point = new Vector3(point.x, point.y + y_ofset, point.z);
            if (Vector3.Distance(real_point,transform.position) <= 10) change(2);
            else transform.position += transform.forward * speed * Time.deltaTime;
            //transform.position += Vector3.MoveTowards(transform.position, real_point, speed * Time.deltaTime);
    }

    void rotate(Vector3 rotatePoint)
    {
        Quaternion target = Quaternion.Euler(rotatePoint);
        if (transform.rotation == target)
        {
            rotating = false;
        }
        else {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, rotation_speed * Time.deltaTime);
        }
        
    }

    void change(int a) {
        Debug.Log("done");
        moving_state = a;
        checkpoint++;
        rotating = true;
    }
}
