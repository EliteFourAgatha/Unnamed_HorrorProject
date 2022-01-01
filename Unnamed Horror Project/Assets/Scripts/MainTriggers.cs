using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    //Fuse box
    public Light[] breakerLights;
    public GameObject fuseSwitch;
    private Color normalEmissionColor;
    private bool breakerOff = false;
    private bool canUseFuseBox = false;
    //Only storage light mat affected by fuse box trigger
    public Material _storageLightMat;



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
    private string fuseboxString = "Fix the faulty light in storage room 4";
    private string spawnGhostString = "Find safety in the light";

    public enum TriggerType {Darkness, SpawnGhost, MainPaper, FuseBox, DungeonDoor, Closet, StorageLight}
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
        if(canUseFuseBox)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteFuseBoxTrigger();
                Debug.Log("fuse box");
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
            else if(triggerType == TriggerType.SpawnGhost)
            {
                if(spawnGhostTriggerActive)
                {
                    ExecuteSpawnGhostTrigger();
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
        }
    }
    //Trigger for main paper in head office
    public void ExecuteMainPaperTrigger()
    {
        //Play clip even if object deactivated
        AudioSource.PlayClipAtPoint(mainPaperSFX, gameObject.transform.position);
        gameController.currentCheckpoint = 1;
        StartCoroutine(gameController.ShowPopupMessage(paperInstructionString, 2));
        mainPaper.SetActive(false);
    }
    //Trigger to enter final dungeon scene
    public void ExecuteDungeonTrigger()
    {
        triggerAudio.Play();
        levelController.FadeInToLevel(2);
    }
    public void ExecuteFuseBoxTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        if(gameController.currentCheckpoint == 1)
        {
            gameController.currentCheckpoint = 2;
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
        //Rock wall crumbles behind player
        //sound of rocks tumbling to ground
        //wisp of wind, short.
        //all 'entrance lights' go out at once.
        //hearbeat sfx + unsettilng music or ambience
        //Ghost spawn animation, maybe cutscene if necessary
        //Otherwise, ghost slowly starts to patrol and try to find player
        // Activate ghost AI.
        spawnGhostTriggerActive = false; //single use, deactivate after

    }
    IEnumerator DelayFadeToLevel(float delayTime, int levelNumber)
    {
        yield return new WaitForSeconds(delayTime);
        levelController.FadeInToLevel(levelNumber);
    }
}
