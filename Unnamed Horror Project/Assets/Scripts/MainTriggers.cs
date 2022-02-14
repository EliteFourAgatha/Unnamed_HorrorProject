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
    public GameObject closetLightBulb;
    public GameObject fakeCloset;  
    private bool darknessTriggerActive = true;

    
    //Spawn Ghost
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
    public GameObject closetDoor;
    public GameObject lockedClosetDoorTrigger;
    private Animator closetDoorAnim;
    private bool closetTriggerActive = true;
    public Light backroomLightOne;
    public Light backroomLightTwo;
    public AudioClip lightShatterSFX;



    private bool canOpenDungeonDoor = false;
    private string paperInstructionString = "Press [Esc] to view objectives";
    private string laundryWindowString = "Turn off breaker to storage area";
    private string fuseboxString = "Fix the faulty light in storage room 4";
    private string spawnGhostString = "Find safety in the light";

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
        if(closetDoorAnim == null)
        {
            closetDoorAnim = closetDoor.GetComponent<Animator>();
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
        //Play clip even if object deactivated
        AudioSource.PlayClipAtPoint(mainPaperSFX, gameObject.transform.position);
        gameController.currentCheckpoint = 1;
        laundryWindowTrigger.SetActive(true);
        fuseBoxTrigger.SetActive(true);
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
            StartCoroutine(gameController.ShowPopupMessage(laundryWindowString, 2));
        }
    }
    public void ExecuteFuseBoxTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        if(gameController.currentCheckpoint == 2)
        {
            gameController.currentCheckpoint = 3;
            StartCoroutine(gameController.ShowPopupMessage(fuseboxString, 2));
            //Turn lights on
            if(!breakerOn)
            {
                breakerOn = true;
                foreach(Light light in breakerLights)
                {
                    light.enabled = true;
                }
            }
            //Turn lights off
            else
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
            //Turn lights on
            if(!breakerOn)
            {
                breakerOn = true;
                foreach(Light light in breakerLights)
                {
                    light.enabled = true;
                }
            }
            //Turn lights off
            else
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
        //Lights go out
        closetLightBulb.SetActive(false);
        fakeCloset.SetActive(false);
        //Disable darkness backdrop
        closetDarkBackdrop.SetActive(false);
        closetDarkTunnel.SetActive(true);

        //start animation for walls closing in here
        //darknessWall.getcomponent<animator>?


        //Spooky audio plays, need better SFX
        triggerAudio.Play();


        darknessTriggerActive = false; //single use, deactivate after
    }
    //Called by animation event after walls close in
    public void FadeToHallucination()
    {
        levelController.FadeInToLevel(2);
    }
    public void ExecuteFinalClosetTrigger()
    {
        StartCoroutine(ShatterLightsBeforeCloset());
        StartCoroutine(OpenClosetAfterDelay(2f));
        lockedClosetDoorTrigger.SetActive(false);

        closetTriggerActive = false; //single use, deactivate after
    }
    //spawn ghost in level 2. player now has to run for his life
    public void ExecuteSpawnGhostTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        StartCoroutine(gameController.ShowPopupMessage(spawnGhostString, 2));
        spawnGhostTriggerActive = false; //single use, deactivate after

    }
    public void ExecuteFirstScare()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        shadowBlurMonster.SetActive(true);
        shadowTriggerActive = false; // single use, deactivate after



        //enable top of stairs door. move this to actual spot later, based on
        //  flow of logic. (if player doesn't leave by x point, spawn this door)
        topOfStairsDoor.SetActive(true);
    }
    IEnumerator DelayFadeToLevel(float delayTime, int levelNumber)
    {
        yield return new WaitForSeconds(delayTime);
        levelController.FadeInToLevel(levelNumber);
    }
    IEnumerator WaitAndDisableMainPaper()
    {
        StartCoroutine(gameController.ShowPopupMessage(paperInstructionString, 1.5f));
        yield return new WaitForSeconds(0.5f);
        mainPaper.SetActive(false);
    }
    IEnumerator ShatterLightsBeforeCloset()
    {
        backroomLightOne.enabled = false;
        AudioSource.PlayClipAtPoint(lightShatterSFX, transform.position);
        yield return new WaitForSeconds(1f);
        backroomLightTwo.enabled = false;
        AudioSource.PlayClipAtPoint(lightShatterSFX, transform.position);
    }
    IEnumerator OpenClosetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        closetDoorAnim.Play("ClosetCreakOpen");
    }
}
