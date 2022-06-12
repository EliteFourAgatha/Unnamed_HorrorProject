using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private LevelController levelController;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject monsterObjectRef;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform hideLocation;
    [SerializeField] private Transform unHideLocation;
    [SerializeField] private Animation playerAnim;
    [SerializeField] private MonsterAI monsterAI;
    private AudioSource triggerAudio;

    [Header("Triggerable Sounds")]
    bool soundTriggerCanFire = true;
    AudioClip chosenClip;
    [SerializeField] private AudioClip[] woodCreakClips;
    [SerializeField] private AudioClip[] buildingGroanClips;
    [SerializeField] private AudioClip[] pebbleDropClips;

    [Header("Locker Door")]
    [SerializeField] private Animator lockerDoorAnim;
    public bool lockerDoorClosed = true;


    [Header("Hiding Trigger")]
    [SerializeField] private GameObject chosenLockerDoor;
    
    [Header("Laundry Window")]
    [SerializeField] private Animator laundryWindowAnim;
    [SerializeField] private AudioSource laundryRainAudio;
    public bool canCloseWindow = true;
    public bool windowFixed = false;

    [Header("Head Office Locked Drawer")]
    [SerializeField] private Animator lockedDrawerAnim;
    [SerializeField] private Collider drawerBoxCollider;
    [SerializeField] private GameObject underDrawerCollider;
    [SerializeField] private GameObject noteOne;
    [SerializeField] private GameObject noteTwo;
    [SerializeField] private GameObject expositionUIOne;
    [SerializeField] private GameObject expositionUITwo;
    [SerializeField] private MeshRenderer noteOneMeshRenderer;
    [SerializeField] private MeshRenderer noteTwoMeshRenderer;

    [SerializeField] private enum TriggerType {NormalObject, LockerHide, WoodCreak, PebbleDrop,
                                ShufflingFootsteps, Exposition1, Exposition2}
    [SerializeField] private TriggerType triggerType;



    private bool canInteractWithObject = false;
  

    void Start()
    {
        triggerAudio = gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //If player inside of locker...
            if(triggerType == TriggerType.LockerHide)
            {
                if(monsterAI.enabled)
                {
                    var triggerRef = chosenLockerDoor.GetComponent<Triggers>();
                    
                    monsterAI.playerIsHiding = true;
                    //If locker door is closed...
                    if(triggerRef.lockerDoorClosed)
                    {
                        if(monsterAI.playerCanHide)
                        {
                            //Player is hidden from monster's view
                            monsterAI.playerIsHiding = true;
                        }
                    }
                }
            }
            else if(triggerType == TriggerType.WoodCreak)
            {
                if(soundTriggerCanFire)
                {
                    ExecuteSoundTrigger();
                }
            }
            else if(triggerType == TriggerType.PebbleDrop)
            {
                if(soundTriggerCanFire)
                {
                    ExecuteSoundTrigger();
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
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
            if(triggerType == TriggerType.WoodCreak)
            {
                StartCoroutine(PlaySoundTriggerAndCooldown(woodCreakClips));
            }
            else if(triggerType == TriggerType.PebbleDrop)
            {
                StartCoroutine(PlaySoundTriggerAndCooldown(pebbleDropClips));
            }
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
        canCloseWindow = false;
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        laundryWindowAnim.Play("CloseWindow");
        laundryRainAudio.volume = 0.3f; //Muffle outside sfx
    }

    public void InteractWithDrawer()
    {
        lockedDrawerAnim.Play("DrawerOpen", 0, 0.0f);
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        underDrawerCollider.SetActive(true);
        drawerBoxCollider.enabled = false;
    }

    //Exposition note #1 in head office
    public void InteractWithExpositionNote()
    {
        if(triggerType == TriggerType.Exposition1)
        {
            if(!triggerAudio.isPlaying)
            {
                triggerAudio.Play();
            }
            noteOneMeshRenderer.enabled = false;
            expositionUIOne.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            noteOne.tag = "Untagged";
        }
        else if(triggerType == TriggerType.Exposition2)
        {
            if(!triggerAudio.isPlaying)
            {
                triggerAudio.Play();
            }
            noteTwoMeshRenderer.enabled = false;
            expositionUITwo.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            noteTwo.tag = "Untagged";
        }
    }

    // For 
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