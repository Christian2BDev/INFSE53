using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public int maxdistance;
    public LayerMask hittable_items;

  

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            GetComponent<health>().Damage();
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxdistance, hittable_items))
            {
                Debug.Log(hit.collider.gameObject.name + " got hit");
                Debug.DrawRay(transform.position, transform.forward, Color.green, 10000f); print("Hit");
                
            }
        }
    }

   
}

