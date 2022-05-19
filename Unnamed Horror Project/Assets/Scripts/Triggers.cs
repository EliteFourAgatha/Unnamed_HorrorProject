using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour
{
    public GameController gameController;
    public LevelController levelController;
    public Animation playerAnim;
    public AudioSource triggerAudio;
    public MonsterAI monsterAI;
    public enum TriggerType {NormalObject, LockerHide, CouchHide, TriggerWoodCreak,
                                TriggerBuildingGroan, TriggerPebbleDrop}
    public TriggerType triggerType;


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
    
    [Header("Laundry Window")]
    public Animator laundryWindowAnim;
    public AudioSource laundryRainAudio;
    public AudioClip closedWindowSFX;
    bool canUseWindow = true;

    [Header("Head Office Locked Drawer")]
    public Animator lockedDrawerAnim;
    private bool drawerOpen = false;
    public AudioClip openDrawerClip;
    public AudioClip closeDrawerClip;

    [Header("Couch")]
    bool couchHiding = false;



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
    public void InteractWithCouch()
    {
        if(couchHiding)
        {
            StartCoroutine(HideBehindCouch());
        }
        else
        {
            StartCoroutine(UnhideBehindCouch());
        }
    }
    public void InteractWithLocker()
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
    public void CloseLaundryWindow()
    {
        if(canUseWindow)
        {
            canUseWindow = false;
            if(!triggerAudio.isPlaying)
            {
                triggerAudio.Play();
            }
            laundryWindowAnim.Play("CloseWindow");
            laundryRainAudio.volume = 0.3f; //Muffle outside sfx
        }
        else
        {
            triggerAudio.clip = closedWindowSFX;
            //show pop up text "it's nailed shut!" or something
            if(!triggerAudio.isPlaying)
            {
                triggerAudio.Play();
            }
        }

    }
    public void InteractWithDrawer()
    {
        if(drawerOpen)
        {
            lockedDrawerAnim.Play("DrawerClose", 0, 0.0f);
            drawerOpen = false;
            triggerAudio.clip = closeDrawerClip;
            if(!triggerAudio.isPlaying)
            {
                triggerAudio.Play();
            }
        }
        else
        {
            lockedDrawerAnim.Play("DrawerOpen", 0, 0.0f);
            drawerOpen = true;
            triggerAudio.clip = openDrawerClip;
            if(!triggerAudio.isPlaying)
            {
                triggerAudio.Play();
            }
        }
        //Once drawer has been opened for first time / used key...
        gameController.playerNeedsKey = false;
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
    private IEnumerator HideBehindCouch()
    {
        levelController.FadeToBlack();
        //playercontroller.canmove = false;
        yield return new WaitForSeconds(2f);
        levelController.FadeInFromBlack();
        //playercontroller.canmove = true;
    }
}