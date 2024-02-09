using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class shooting : MonoBehaviour
{
    public int maxdistance;
    public LayerMask hittable_items;
    public int maxAmmo;
    public int ammo;
    public TMP_Text ammoTxt;
    public GameObject outTxt;

    void Start()
    {
        StartCoroutine(Fade());
        ammo = maxAmmo;
        ammoTxt.text = ammo.ToString() + "/" + maxAmmo.ToString();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            if (ammo > 0) shoot();
            else StopAllCoroutines();
        }
    }

    void shoot() {
        //debug health
        GetComponent<health>().Damage();
        //----------
        ammo--;
        ammoTxt.text = ammo.ToString() + "/" + maxAmmo.ToString();
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxdistance, hittable_items))
            {
                Debug.Log(hit.collider.gameObject.name + " got hit");
                Debug.DrawRay(transform.position, transform.forward, Color.green, 10000f); print("Hit");

        }
        
    }

   

    IEnumerator Fade()
    {
        Debug.Log("flik");
        yield return new WaitForSeconds(10);
        StopCoroutine(Fade());
        StartCoroutine(Fade());

    }


}

