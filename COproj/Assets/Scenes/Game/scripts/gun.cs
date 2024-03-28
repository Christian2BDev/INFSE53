using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class gun : MonoBehaviour
{
    //dit script doet alles wat te maken heeft met het geweer,
    // van schieten tot het model en animaties

    private enum Hand {
        Left,
        Right
    }
    [SerializeField] private Hand hand;
  
    public AudioSource shootAudio;
    public AudioSource noAmmoAudio;
    OVRHapticsClip shootSoundHaptic;
    OVRHapticsClip noAmmoSoundHaptic;
    public GameObject HealtmMenu;
    public int ActiveGun = 0;

    // er word een lijst gecreerd van de wapens class die ik heb gemaakt hieronder aan de file
    // deze class bevat allerlij informatie over een geweer
    public List<guns> guns = new List<guns>();


    private GameObject GameController;

    //zodra de game word opgestart worden eeerst een paar dingen ge initializeerd,
    // de gamecontroller word gevonden, en de vibraties van het schietn worden gecreerd
    void Start()
    {
        GameController = GameObject.Find("GameController");
        shootSoundHaptic = new OVRHapticsClip(shootAudio.clip);
        noAmmoSoundHaptic = new OVRHapticsClip(noAmmoAudio.clip);

    }

    // dit script kijkt of dit script voor de linker hand of rechter hand moet kijken, dit was een oplossing voor een bug die we hadden
    void Update()
    {
        if (hand == Hand.Right && !GameController.GetComponent<Pause>().paused) Right();
        if (hand == Hand.Left && !GameController.GetComponent<Pause>().paused) Left();
    }

    // in deze methode word alles geregeld voor de rechter hand, als de schiet knop is ingedrukt. en de ammonutie van het geweer is geen 0
    // word de shoot functie aangeroepen. daarnaast speelt hij de vibraties af . an start hij de flash animatie.

    // als knop 1 word ingedrukt word de functie changeGun aangeroepen om het geweer te veranderen

    // als de reload knop word ingedrukt word de functie reload aangeroepen en worden de reload vibraties afgespeeld
    void Right() {
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.RightArrow)) changeGun();


        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown(KeyCode.RightShift))
        {
            guns[ActiveGun].ammo--;
            if (guns[ActiveGun].ammo > 0)
            {
                shoot();
                OVRHaptics.RightChannel.Preempt(shootSoundHaptic);
                guns[ActiveGun].flash.SetActive(true);
                StartCoroutine(flashing());
            }
            else dontShoot(); 
           
           
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyDown(KeyCode.RightControl))
        {
            reload();
            OVRHaptics.RightChannel.Preempt(noAmmoSoundHaptic);
           
        }
    }
    // zelfde als voor rechts, maar dan met andere controlls machend voor linker hand
    void Left() {
        if (OVRInput.GetDown(OVRInput.Button.Three) || Input.GetKeyDown(KeyCode.LeftArrow)) changeGun();
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            guns[ActiveGun].ammo--;
            if (guns[ActiveGun].ammo > 0)
            {
                shoot();
                OVRHaptics.LeftChannel.Preempt(shootSoundHaptic);
                guns[ActiveGun].flash.SetActive(true);
                StartCoroutine(flashing());
            }
            else dontShoot();

            
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            reload();
            OVRHaptics.LeftChannel.Preempt(noAmmoSoundHaptic);
        }

    }
    //flash animatie
    IEnumerator flashing() {

        yield return new WaitForSeconds(0.1f);
        guns[ActiveGun].flash.SetActive(false);
    }

    //hier gebeurd het schieten, de animatie word agespeeld van schieten. het geluid ook.
    // zodra een object word geraakt kijkt hij of het een zombie is, zo ja
    // roept hij de damage functie aan op de zombie, en voert de damage variable door van de op dit moment actieve geweer
    void shoot() {

        guns[ActiveGun].GunAnimator.SetBool("shoot", true);
            //GunAnimator.SetBool("shoot", false);
            StartCoroutine(playAnimation());
            shootAudio.Play();
           
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                GameObject hitObj = hit.collider.gameObject;
                Debug.Log(hitObj.name + " got hit");
                if (hitObj.tag.Equals("enemy"))
                {

                hitObj.GetComponent<Zombie>().DamageEnemy(guns[ActiveGun].Damage);
                
                }

            }
    }
    // deze methode speelt de animatie af dat je niet kan schieten omdat je geen ammo hebt
    void dontShoot() {
        noAmmoAudio.Play();
        guns[ActiveGun].GunAnimator.SetBool("noammo", true);
    }
    // deze methode reload het geweer, wat inhoud dat de ammonutie weer het maximale word
    void reload() {
        guns[ActiveGun].ammo = guns[ActiveGun].maxAmmo;
        guns[ActiveGun].GunAnimator.SetBool("noammo", false);

    }

    // speel schiet animatie af
    IEnumerator playAnimation()
    {

        yield return new WaitForSeconds(0.1f);
        guns[ActiveGun].GunAnimator.SetBool("shoot", false);
    }

    // deze methode laad je je geweer veranderen, naar de volgende in de lijst
    void changeGun() {
        if (ActiveGun == guns.Count - 1)
        {
            ActiveGun = 0;
        }
        else {
            ActiveGun++;
        }

        foreach (var gun in guns)
        {
            if (guns.IndexOf(gun) == ActiveGun)
            {
                gun.GunModel.SetActive(true);
            }
            else {
            gun.GunModel.SetActive(false);
            }
        }
    }
}

[System.Serializable]
public class guns {
    // dit is alle data van 1 gun
    public string name;
    public int ammo;
    public int maxAmmo;
    public int Damage;
    public Animator GunAnimator;
    public GameObject GunModel;
    public GameObject flash;
}

