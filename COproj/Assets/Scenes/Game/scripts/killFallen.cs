using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killFallen : MonoBehaviour
{
    //bug methode, zorgt ervoor dat als op er op een of andere manier een zombie door de vloer
    // is gezakt, dat hij verwijderd word
    private void OnCollisionEnter(Collision collision)
    {
        
            Destroy(collision.gameObject);
        
    }
}
