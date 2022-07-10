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

    [Header("Locker Door")]
    [SerializeField] private Animator lockerDoorAnim;
    [SerializeField] private GameObject chosenLockerDoor;
    public bool lockerDoorClosed = true;
    
    [Header("Laundry Window")]
    [SerializeField] private Animator laundryWindowAnim;
    [SerializeField] private AudioSource laundryRainAudio;
    public bool canCloseWindow = true;
    public bool windowFixed = false;

    [Header("Head Office Locked Drawer")]
    [SerializeField] private Animator lockedDrawerAnim;
    [SerializeField] private Collider drawerBoxCollider;
    [SerializeField] private GameObject underDrawerCollider;
    [SerializeField] private GameObject expositionUIOne;

    [SerializeField] private enum TriggerType {NormalObject, LockerHide, ExpositionJournal,
                                                SewerWater, InhaleScare}
    [SerializeField] private TriggerType triggerType;

    //private bool inhaleTriggerActive = true;

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
            else if(triggerType == TriggerType.SewerWater)
            {
                playerController.playerInWater = true;
            }
            else if(triggerType == TriggerType.InhaleScare)
            {

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
        else if(triggerType == TriggerType.SewerWater)
        {
            playerController.playerInWater = false;
        }
    }
    public void TriggerAudioOnly() //Interact with objects + sfx only (locked door)
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
    }
    public void InteractWithLocker()
    {
        if(lockerDoorAnim == null)
        {
            lockerDoorAnim = gameObject.GetComponent<Animator>();
        }

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

    public void InteractWithExpositionNote()
    {
        if(triggerType == TriggerType.ExpositionJournal)
        {
            if(!triggerAudio.isPlaying)
            {
                triggerAudio.Play();
            }
            expositionUIOne.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}