using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour
{
    public Animation playerAnim;
    public AudioSource triggerAudio;
    public MonsterAI monsterAI;
    public enum TriggerType {NormalObject, LockerHide, FireScare, CouchHide, TriggerWoodCreak,
                                TriggerBuildingGroan, TriggerPebbleDrop}
    public TriggerType triggerType;

    [Header("Chamber Lights")]
    private bool chamberLightTriggerActive = true;
    public GameObject affectedChamberLights;
    private bool fireScareTriggerActive = true;
    public GameObject fireScareObject;

    [Header("Laundry Window")]
    public Animator laundryWindowAnim;
    private bool laundryWindowOpen = true;
    public AudioClip openWindowSFX;
    public AudioClip closeWindowSFX;
    public AudioSource laundryRainAudio;


    [Header("Triggerable Sounds")]
    bool soundTriggerCanFire = true;
    AudioClip chosenClip;
    public AudioClip[] woodCreakClips;
    public AudioClip[] buildingGroanClips;
    public AudioClip[] pebbleDropClips;

    [Header("Locker Door")]
    public Animator lockerDoorAnim;
    public bool lockerDoorClosed = true;

    [Header("Hiding Trigger")]
    public GameObject chosenLockerDoor;



    private bool canInteractWithObject = false;

    void Start()
    {
        if(triggerAudio == null)
        {
            triggerAudio = gameObject.GetComponent<AudioSource>();
        }
        if(playerAnim == null)
        {
            playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation>();
        }
    }
    void Update()
    {
        if(canInteractWithObject)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                TriggerAudioOnly();
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(triggerType == TriggerType.LockerHide)
            {
                if(monsterAI.enabled)
                {
                    var triggerRef = chosenLockerDoor.GetComponent<Triggers>();
                    
                    monsterAI.playerIsHiding = true;
                    /*
                    //If locker door is closed...
                    if(triggerRef.lockerDoorClosed)
                    {   
                        //Player is hidden from monster's view
                        monsterAI.playerIsHiding = true;
                    }
                    */

                }
            }
            else if(triggerType == TriggerType.FireScare)
            {
                if(fireScareTriggerActive)
                {
                    ExecuteFireScareTrigger();
                }
            }
            else if(triggerType == TriggerType.TriggerWoodCreak)
            {
                if(soundTriggerCanFire)
                {
                    ExecuteSoundTrigger();
                }
            }
            else if(triggerType == TriggerType.TriggerPebbleDrop)
            {
                if(soundTriggerCanFire)
                {
                    ExecuteSoundTrigger();
                }
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(triggerType == TriggerType.LockerHide)
        {
            if(monsterAI.enabled)
            {
                monsterAI.playerIsHiding = false;
            }
        }
    }
    public void TriggerAudioOnly() //Interact with objects + sfx only (locked door)
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
    }
    public void ExecuteSoundTrigger() //Triggerable sound with cooldown
    {
        if(soundTriggerCanFire)
        {
            if(triggerType == TriggerType.TriggerWoodCreak)
            {
                StartCoroutine(PlaySoundTriggerAndCooldown(woodCreakClips));
            }
            else if(triggerType == TriggerType.TriggerPebbleDrop)
            {
                StartCoroutine(PlaySoundTriggerAndCooldown(pebbleDropClips));
            }
        }
    }
    public void ExecuteChamberLightsTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        affectedChamberLights.SetActive(true);
        chamberLightTriggerActive = false;
    }
    public void ExecuteFireScareTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        affectedChamberLights.SetActive(true);
        StartCoroutine(FlashFireScareObject());
        fireScareTriggerActive = false;
    }
    public void TriggerLockerDoor()
    {
        if(lockerDoorAnim == null)
        {
            lockerDoorAnim = gameObject.GetComponent<Animator>();
        }
        //randomized, at least 3 clips (4-6 ideal)
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        if(lockerDoorClosed)
        {
            lockerDoorClosed = false;
            lockerDoorAnim.Play("OpenLockerDoor");
        }
        else
        {
            lockerDoorClosed = true;
            lockerDoorAnim.Play("CloseLockerDoor");
        }
    }
    public void TriggerLaundryWindow()
    {
        if(laundryWindowOpen)
        {
            triggerAudio.clip = openWindowSFX;
            laundryWindowAnim.Play("CloseWindow");
            laundryWindowOpen = false;

            laundryRainAudio.volume = 0.3f; //Muffle outside sfx
        }
        else
        {
            triggerAudio.clip = closeWindowSFX;
            laundryWindowAnim.Play("OpenWindow");
            laundryWindowOpen = true;

            laundryRainAudio.volume = 0.8f; //Reverse above
        }
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
    }
    private IEnumerator FlashFireScareObject()
    {
        fireScareObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fireScareObject.SetActive(false);
    }
    private IEnumerator PlaySoundTriggerAndCooldown(AudioClip[] sourceArray)
    {
        int randInt = Random.Range(0, sourceArray.Length);
        chosenClip = sourceArray[randInt];
        triggerAudio.clip = chosenClip;

        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        soundTriggerCanFire = false;
        yield return new WaitForSeconds(5);
        soundTriggerCanFire = true;
    }
}