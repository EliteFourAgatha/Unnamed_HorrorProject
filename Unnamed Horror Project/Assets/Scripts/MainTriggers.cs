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
    public GameObject closetDarkBackdrop;
    public GameObject closetLightBulb;
    public GameObject fakeCloset;
    public GameObject dungeonDoor;    
    private bool darknessTriggerActive = true;

    
    //Spawn Ghost
    public GameObject spawnedGhost;
    private bool spawnGhostTriggerActive = true;


    //Main Paper
    public GameObject mainPaper;    
    private bool canGrabPaper = false;
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
    public GameObject fuseSwitch;
    private Color normalEmissionColor;
    private bool breakerOff = false;
    private bool canUseFuseBox = false;
    //Only storage light mat affected by fuse box trigger
    public Material _storageLightMat;


    //Shadow scare
    public GameObject shadow;
    private bool shadowTriggerActive = true;
    public GameObject shadowHideTimer;
    public GameObject shadowHideTriggers;
    private bool canHideFromShadow = false;
    



    //Open closet
    public MeshRenderer bathroomLightMesh;
    public MeshRenderer brokenWindowMesh;
    private Material bathroomLightMat;
    private Material brokenWindowMat;
    public Light bathroomLight;
    public GameObject closetDoor;
    private bool closetTriggerActive = true;


    private bool canOpenDungeonDoor = false;
    private string paperInstructionString = "Press [Esc] to view objectives";
    private string laundryWindowString = "Turn off breaker to storage area";
    private string fuseboxString = "Fix the faulty light in storage room 4";
    private string spawnGhostString = "Find safety in the light";

    public enum TriggerType {Darkness, SpawnGhost, MainPaper, LaundryWindow, FuseBox, DungeonDoor, Closet, StorageLight,
                                ShadowScare, ShadowHide}
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
        if(triggerType == TriggerType.Closet)
        {
            brokenWindowMat = brokenWindowMesh.material;
            bathroomLightMat = bathroomLightMesh.material;
        }
    }
    void Update()
    {
        if(canGrabPaper)
        {
            //If object material is currently highlighted, means player is looking at it
            // Check for that before E input. If object = highlighted...
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteMainPaperTrigger();
            }
        }
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
        //If player enters "hide" collider on wall...
        if(canHideFromShadow)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteHideTrigger();
                canHideFromShadow = false;
                shadowHideTimer.SetActive(false); // Turn off timer UI
                shadowHideTriggers.SetActive(false); // Turn off hide triggers
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
                    ExecuteClosetTrigger();
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
                    ExecuteShadowScareTrigger();
                }
            }
            else if(triggerType == TriggerType.MainPaper)
            {
                canGrabPaper = true;
            }
            else if(triggerType == TriggerType.DungeonDoor)
            {
                canOpenDungeonDoor = true;
            }
            else if(triggerType == TriggerType.FuseBox)
            {
                canUseFuseBox = true;
            }
            else if(triggerType == TriggerType.ShadowHide)
            {
                canHideFromShadow = true;
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
            if(triggerType == TriggerType.MainPaper)
            {
                canGrabPaper = false;
            }
            else if(triggerType == TriggerType.DungeonDoor)
            {
                canOpenDungeonDoor = false;
            }
            else if(triggerType == TriggerType.FuseBox)
            {
                canUseFuseBox = false;
            }
            else if(triggerType == TriggerType.ShadowHide)
            {
                canHideFromShadow = false;
            }
            else if(triggerType == TriggerType.LaundryWindow)
            {
                canUseLaundryWindow = false;
            }
        }
    }
    //Trigger for main paper in head office
    public void ExecuteMainPaperTrigger()
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
        }
        //Turn lights on
        if(breakerOff)
        {
            breakerOff = false;
            fuseSwitch.transform.eulerAngles = new Vector3(-60, 0, 0); //Breaker switch ON
            foreach(Light light in breakerLights)
            {
                light.enabled = true;
            }
            _storageLightMat.SetColor("_EmissionColor", normalEmissionColor);
        }
        //Turn lights off
        else
        {
            breakerOff = true;
            fuseSwitch.transform.eulerAngles = new Vector3(-20, 0, 0); //Breaker switch OFF
            foreach(Light light in breakerLights)
            {
                light.enabled = false;
            }
            _storageLightMat.SetColor("_EmissionColor", Color.black);
        }
    }
    public void ExecuteDarknessTrigger()
    {
        //Set darkness wall active behind player, block escape backwards
        darknessWall.SetActive(true);
        //Lights go out
        closetLightBulb.SetActive(false);
        fakeCloset.SetActive(false);
        //Enable dungeon door
        dungeonDoor.SetActive(true);
        //Disable darkness backdrop
        closetDarkBackdrop.SetActive(false);
        //Spooky audio plays, need better SFX
        triggerAudio.Play();
        darknessTriggerActive = false; //single use, deactivate after
    }
    public void ExecuteClosetTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        //Enable broken window emissive color
        brokenWindowMat.SetColor("_EmissionColor", new Color (0.1f, 0.1f, 0.1f, 1f));
        bathroomLightMat.SetColor("_EmissionColor", Color.black);
        bathroomLight.enabled = false;
        //
        // Missing, complete later
        //open closet door (slowly) with creaky door sfx
        //
        //
        closetTriggerActive = false; //single use, deactivate after
    }
    public void ExecuteSpawnGhostTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        StartCoroutine(gameController.ShowPopupMessage(spawnGhostString, 2));
        spawnGhostTriggerActive = false; //single use, deactivate after

    }
    public void ExecuteShadowScareTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        //Get distance away from storage lights and fade them
        //  based on range. As shadow moves down hallway, it appears to bend light around it.
        // Maybe player can't move in this moment? frozen in fear?
        //  (disable movement / ability to turn camera away)
        shadow.SetActive(true);
        shadowHideTimer.SetActive(true);
        shadowHideTriggers.SetActive(true);
        shadowTriggerActive = false; // single use, deactivate after
    }
    public void ExecuteHideTrigger()
    {
        //Rotate player towards fixed point in center of hallway (player's back to wall)
        //Lock 'manual' player movement / rotation
        //Move player until their back is touching wall
        //Big breaths of air? heavy breathing?
        //Shadow stops, looks down hall, then keeps moving
        Debug.Log("successfuly hid!");
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
}
