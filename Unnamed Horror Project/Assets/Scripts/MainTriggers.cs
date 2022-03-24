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


    //Darkness
    public GameObject darknessWall;
    public GameObject darknessWallTwo;
    public GameObject closetDarkBackdrop;
    public GameObject closetDarkTunnel;
    public GameObject closetGrimReaper;
    public GameObject closetLightBulb;
    public GameObject fakeCloset;  
    private bool darknessTriggerActive = true;

    
    //Spawn Ghost in level 2
    public GameObject spawnedGhost;
    private bool spawnGhostTriggerActive = true;

    public GameObject topOfStairsDoor;


    //Main Paper
    public GameObject mainPaper;    
    public AudioClip mainPaperSFX;
    public GameObject laundryWindowTrigger;
    public GameObject fuseBoxTrigger;


    //Laundry Window
    public Animator laundryWindowAnim;
    private bool canUseLaundryWindow = false;
    private bool laundryWindowOpen = true;
    public AudioClip openWindowSFX;
    public AudioClip closeWindowSFX;


    //Fuse box
    public Light[] breakerLights;
    public bool breakerOn = true;
    private bool canUseFuseBox = false;


    //First scare - motion blur
    public GameObject shadowBlurMonster;
    private bool shadowTriggerActive = true; 



    //Open closet
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



    private bool canOpenDungeonDoor = false;
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
    void Update()
    {
        if(canOpenDungeonDoor)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteDungeonTrigger();
            }
        }
        if(canUseLaundryWindow)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteLaundryWindowTrigger();
            }
        }
        if(canUseFuseBox)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteFuseBoxTrigger();
            }
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
                    ExecuteDarknessTrigger();
                }
            }
            else if(triggerType == TriggerType.Closet)
            {
                if(closetTriggerActive)
                {
                    ExecuteFinalClosetTrigger();
                }
            }
            //Spawn ghost in final maze
            else if(triggerType == TriggerType.SpawnGhost)
            {
                if(spawnGhostTriggerActive)
                {
                    ExecuteSpawnGhostTrigger();
                }
            }
            //Shadow scare after fixing light
            else if(triggerType == TriggerType.ShadowScare)
            {
                if(shadowTriggerActive)
                {
                    ExecuteFirstScare();
                }
            }
            else if(triggerType == TriggerType.DungeonDoor)
            {
                canOpenDungeonDoor = true;
            }
            else if(triggerType == TriggerType.FuseBox)
            {
                canUseFuseBox = true;
            }
            else if(triggerType == TriggerType.LaundryWindow)
            {
                canUseLaundryWindow = true;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(triggerType == TriggerType.DungeonDoor)
            {
                canOpenDungeonDoor = false;
            }
            else if(triggerType == TriggerType.FuseBox)
            {
                canUseFuseBox = false;
            }
            else if(triggerType == TriggerType.LaundryWindow)
            {
                canUseLaundryWindow = false;
            }
        }
    }
    //Trigger for main paper in head office
    public void PickUpMainPaper()
    {
        StartCoroutine(WaitAndDisableMainPaper());
    }
    //Trigger to enter final dungeon scene
    public void ExecuteDungeonTrigger()
    {
        triggerAudio.Play();
        levelController.FadeInToLevel(2);
    }
    public void ExecuteLaundryWindowTrigger()
    {
        if(laundryWindowOpen)
        {
            triggerAudio.clip = openWindowSFX;
            laundryWindowAnim.SetTrigger("CloseWindow");
            laundryWindowOpen = false;
        }
        else
        {
            triggerAudio.clip = closeWindowSFX;
            laundryWindowAnim.SetTrigger("OpenWindow");
            laundryWindowOpen = true;
        }

        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }

        if(gameController.currentCheckpoint == 1)
        {
            gameController.currentCheckpoint = 2;
            StartCoroutine(gameController.ShowPopupMessage(laundryToWrenchString, 2));
        }
    }
    public void ExecuteFuseBoxTrigger()
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
    public void ExecuteDarknessTrigger()
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
    public void ExecuteFinalClosetTrigger()
    {
        StartCoroutine(ShatterLightsBeforeCloset());
        StartCoroutine(OpenClosetAfterDelay(2f));
        lockedClosetDoorTrigger.SetActive(false); //Trigger on closet door

        

        closetTriggerActive = false; //single use, deactivate after
    }

    public void ExecuteSpawnGhostTrigger()  //spawn ghost in level 2
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

    public void ExecuteFirstScare()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
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
    public void ExecuteLockedDrawerTrigger()
    {
        //get animator
        // if drawer closed, open drawer
        // if drawer open, close drawer
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
}
