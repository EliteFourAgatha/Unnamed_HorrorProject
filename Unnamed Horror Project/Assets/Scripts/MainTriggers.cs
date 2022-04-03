using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Consider creating scare trigger script and one for simple item interactions.

public class MainTriggers : MonoBehaviour
{
    public GameController gameController;
    public LevelController levelController;
    public AudioSource triggerAudio;
    public AudioSource musicAudioSource;


    [Header("Darkness Trigger (In Closet)")]
    public GameObject darknessWall;
    public GameObject darknessWallTwo;
    public GameObject closetDarkBackdrop;
    public GameObject closetDarkTunnel;
    public GameObject closetGrimReaper;
    public GameObject closetLightBulb;
    public GameObject fakeCloset;  
    private bool darknessTriggerActive = true;


    [Header("Misc.")]
    public GameObject topOfStairsDoor;


    [Header("Main Paper")]
    public GameObject mainPaper;    
    public AudioClip mainPaperSFX;
    public GameObject laundryWindowTrigger;
    public GameObject fuseBoxTrigger;

    [Header("Fuse Box")]
    public Light[] breakerLights;
    public bool breakerOn = true;
    public AudioSource breathBehindYouAudio;


    [Header("Head Office Key")]
    public GameObject heldKey;
    public GameObject heldKeyUI;

    [Header("Head Office Locked Drawer")]
    public Animator lockedDrawerAnim;
    private bool drawerOpen = false;
    public AudioClip openDrawerClip;
    public AudioClip closeDrawerClip;


    [Header("First Scare")]
    public GameObject shadowBlurMonster;
    private bool shadowTriggerActive = true;
    public AudioSource heartBeatAudioSource; 



    [Header("Open Closet")]
    public GameObject lockedClosetDoorTrigger;
    public Animator closetDoorAnim;
    private bool closetTriggerActive = true;
    public Light[] breakLightsArray;
    public Light backroomLightOne;
    public AudioSource lightOneAudioSource;
    public Light backroomLightTwo;
    public AudioSource lightTwoAudioSource;
    public AudioSource closetCreakAudioSource;
    public AudioClip closetCreakAudio;

    [Header("Level 2 - Spawn Ghost")]
    //Spawn Ghost in level 2
    public GameObject spawnedGhost;
    private bool spawnGhostTriggerActive = true;


    private string paperInstructionString = "Press [Esc] to view objectives";
    private string laundryToWrenchString = "Find the wrench left for you in storage";
    private string wrenchToBathroomString = "Fix leak in bathroom sink";
    private string BathroomToFuseBoxString = "Turn off breaker to storage area";
    private string fuseboxString = "Fix the faulty light in storage room 4";

    public enum TriggerType {Darkness, SpawnGhost, MainPaper, LaundryWindow, FuseBox, DungeonDoor, Closet, StorageLight,
                                ShadowScare}
    public TriggerType triggerType;
    void Start()
    {
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        if (gameController == null)
        {
            levelController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        }
        if(triggerAudio == null)
        {
            triggerAudio = gameObject.GetComponent<AudioSource>();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(triggerType == TriggerType.Darkness)
            {
                if(darknessTriggerActive)
                {
                    TriggerDarkness();
                }
            }
            else if(triggerType == TriggerType.Closet)
            {
                if(closetTriggerActive)
                {
                    TriggerFinalCloset();
                }
            }
            //Spawn ghost in final maze. Red light triggers (rename?)
            else if(triggerType == TriggerType.SpawnGhost)
            {
                if(spawnGhostTriggerActive)
                {
                    TriggerSpawnGhost();
                }
            }
            //Shadow scare after fixing light
            else if(triggerType == TriggerType.ShadowScare)
            {
                if(shadowTriggerActive)
                {
                    TriggerFirstScare();
                }
            }
        }
    }
    public void PickUpMainPaper() // Main paper in head office
    {
        StartCoroutine(WaitAndDisableMainPaper());
    }
    public void PickUpPliers() // Pliers in basement
    {
        if(gameController.currentCheckpoint == 1)
        {
            if(!triggerAudio.isPlaying)
            {
                triggerAudio.Play();
            }
            //StartCoroutine(ShowItemAndText(heldPliers, heldPliersUI, 3f));
            gameController.currentCheckpoint = 2;
            //disable pliers mesh, then delete after delay? Only used once
        }
    }
    //Trigger to enter final dungeon scene
    public void TriggerDungeon()
    {
        triggerAudio.Play();
        levelController.FadeInToLevel(2);
    }
    public void TriggerFuseBox()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        if(gameController.currentCheckpoint == 4)
        {
            gameController.currentCheckpoint = 5;
            StartCoroutine(gameController.ShowPopupMessage(fuseboxString, 2));
            if(!breakerOn)  //Turn lights on
            {
                breakerOn = true;
                foreach(Light light in breakerLights)
                {
                    light.enabled = true;
                }
            }
            else  //Turn lights off
            {
                breakerOn = false;
                foreach(Light light in breakerLights)
                {
                    light.enabled = false;
                }
            }
        }
        else
        {
            if(!breakerOn)  //Turn lights on
            {
                breakerOn = true;
                foreach(Light light in breakerLights)
                {
                    light.enabled = true;
                }
            }
            else  //Turn lights off
            {
                breakerOn = false;
                foreach(Light light in breakerLights)
                {
                    light.enabled = false;
                }
            }
        }
    }
    public void TriggerDarkness()
    {
        //Set darkness wall active behind player, block escape backwards
        darknessWall.SetActive(true);
        darknessWallTwo.SetActive(true);
        closetDarkTunnel.SetActive(true);
        closetGrimReaper.SetActive(true);

        closetLightBulb.SetActive(false);
        fakeCloset.SetActive(false);
        closetDarkBackdrop.SetActive(false);


        //Spooky audio plays, need better SFX
        triggerAudio.Play();

        darknessTriggerActive = false; //single use, deactivate after
    }

    public void FadeToHallucination()  //Animation event after walls close in
    {
        levelController.FadeInToLevel(2);
    }
    public void TriggerFinalCloset()
    {
        StartCoroutine(ShatterLightsBeforeCloset());
        StartCoroutine(OpenClosetAfterDelay(2f));
        lockedClosetDoorTrigger.SetActive(false); //Trigger on closet door

        

        closetTriggerActive = false; //single use, deactivate after
    }

    public void TriggerSpawnGhost()  //spawn ghost in level 2
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        //spawn ghost in first room (starting area)
        // Then tell ghost which quadrant / room player is currently in.

        //
        //    **perhaps instead of going in order, can make a few set paths for
        //     him to travel.**
        //   Path 1: moves around outside, checks perimeter, moves on if nothing
        //   Path 2: moves in zig-zag
    }

    public void TriggerFirstScare()
    {
        musicAudioSource.Stop();
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }

        //Remember to turn this off somewhere. Loses effect the longer it stays on.
        //  Should be for tense moments, then fade out
        if(!heartBeatAudioSource.isPlaying)
        {
            heartBeatAudioSource.Play();
        }

        shadowBlurMonster.SetActive(true);
        shadowTriggerActive = false; // single use, deactivate after
        

        //First scare, blur rushes past.
        // player given option to leave now.
        // if player continues (goes into maintenance room, use simple trigger),
        //  then spawn topofstairsdoor and disable ability to leave.



        //enable top of stairs door. move this to actual spot later, based on
        //  flow of logic. (if player doesn't leave by x point, spawn this door)
        topOfStairsDoor.SetActive(true);
    }
    public void TriggerLockedDrawer()
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
    public void TriggerFoundKey()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        StartCoroutine(ShowItemAndText(heldKey, heldKeyUI, 4f));
        gameController.playerNeedsKey = false;
        //Reset object tag to disable interactions
        gameObject.tag = "Untagged";
    }
    public void ExecuteExpositionNote()
    {
        
    }
    IEnumerator DelayFadeToLevel(float delayTime, int levelNumber)
    {
        yield return new WaitForSeconds(delayTime);
        levelController.FadeInToLevel(levelNumber);
    }
    IEnumerator WaitAndDisableMainPaper()
    {
        //Play clip even if object deactivated
        AudioSource.PlayClipAtPoint(mainPaperSFX, gameObject.transform.position);
        gameController.currentCheckpoint = 1;
        laundryWindowTrigger.SetActive(true);
        fuseBoxTrigger.SetActive(true);
        StartCoroutine(gameController.ShowPopupMessage(paperInstructionString, 1f));
        yield return new WaitForSeconds(1.5f);
        mainPaper.SetActive(false);
    }
    IEnumerator ShatterLightsBeforeCloset()
    {
        //disable light on proximity
        //  if distance between player and one of lights in
        //  breakLights array < breakDistance, disable/SFX.
        //   'darkness is walking with you'
        backroomLightOne.enabled = false;
        lightOneAudioSource.Play();


        yield return new WaitForSeconds(1f);
        backroomLightTwo.enabled = false;
        lightTwoAudioSource.Play();
    }
    IEnumerator OpenClosetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        closetCreakAudioSource.clip = closetCreakAudio;
        closetCreakAudioSource.Play();
        closetDoorAnim.Play("ClosetCreakOpen");
    }
    IEnumerator ShowItemAndText(GameObject item, GameObject text, float delay)
    {
        item.SetActive(true);
        text.SetActive(true);
        yield return new WaitForSeconds(delay);
        item.SetActive(false);
        text.SetActive(false);
    }
}