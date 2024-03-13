using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class gun : MonoBehaviour
{

    private enum Hand {
        Left,
        Right
    }
    [SerializeField] private Hand hand;
    public Animator GunAnimator;
    public int ammo;
    public int maxAmmo;
    public int Damage;
    public AudioSource shootAudio;
    public AudioSource noAmmoAudio;
    OVRHapticsClip shootSoundHaptic;
    OVRHapticsClip noAmmoSoundHaptic;
    public GameObject HealtmMenu;
    public GameObject FlashL;
    public GameObject FlashR;


    private GameObject GameController;

    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.Find("GameController");
        shootSoundHaptic = new OVRHapticsClip(shootAudio.clip);
        noAmmoSoundHaptic = new OVRHapticsClip(noAmmoAudio.clip);

    }

    // Update is called once per frame
    void Update()
    {
        
    
        if (hand == Hand.Right && !GameController.GetComponent<Pause>().paused) Right();
        if (hand == Hand.Left && !GameController.GetComponent<Pause>().paused) Left();
    }

    void Right() {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown(KeyCode.RightShift))
        {
            ammo--;
            if (ammo > 0)
            {
                shoot();
                OVRHaptics.RightChannel.Preempt(shootSoundHaptic);
                FlashR.SetActive(true);
                StartCoroutine(flashingRight());
            }
            else dontShoot(); 
           
           
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyDown(KeyCode.RightControl))
        {
            reload();
            OVRHaptics.RightChannel.Preempt(noAmmoSoundHaptic);
           
        }
    }

    void Left() {
        if (OVRInput.GetDown(OVRInput.Button.Four) || Input.GetKeyDown(KeyCode.C)) { HealtmMenu.SetActive(!HealtmMenu.activeSelf); }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            ammo--;
            if (ammo > 0)
            {
                shoot();
                OVRHaptics.LeftChannel.Preempt(shootSoundHaptic);
                FlashL.SetActive(true);
                StartCoroutine(flashingLeft());
            }
            else dontShoot();

            
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            reload();
            OVRHaptics.LeftChannel.Preempt(noAmmoSoundHaptic);
        }

    }

    IEnumerator flashingRight() {

        yield return new WaitForSeconds(0.1f);
        FlashR.SetActive(false);
    }
    IEnumerator flashingLeft()
    {

        yield return new WaitForSeconds(0.1f);
        FlashL.SetActive(false);
    }

    void shoot() {
        
            GunAnimator.SetBool("shoot", true);
            //GunAnimator.SetBool("shoot", false);
            StartCoroutine(playAnimation());
            shootAudio.Play();
           
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                GameObject hitObj = hit.collider.gameObject;
                Debug.Log(hitObj.name + " got hit");
                if (hitObj.tag.Equals("enemy"))
                {

                hitObj.GetComponent<Zombie>().DamageEnemy(Damage);
                
                }

            }
    }

    void dontShoot() {
        noAmmoAudio.Play();
        GunAnimator.SetBool("noammo", true);
    }

    void reload() {
        ammo = maxAmmo;
        GunAnimator.SetBool("noammo", false);

    }

    IEnumerator playAnimation()
    {

        yield return new WaitForSeconds(0.1f);
        GunAnimator.SetBool("shoot", false);
    }
}
