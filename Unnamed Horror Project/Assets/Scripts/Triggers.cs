using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour
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

    
    //Spawn Maze
    public GameObject spawnedMaze;


    //Main Paper
    public GameObject mainPaper;    
    private bool canGrabPaper = false;


    //Light switches
    public Light[] controlledLights;
    public GameObject lightSwitch;
    public MeshRenderer[] lightFoundations;
    private Material[] lightFoundationMaterials;
    private Color normalEmissionColor;
    private bool lightsOff = true;
    private bool canUseLightSwitch = false;


    //Fuse box
    public Light[] breakerLights;
    public GameObject fuseSwitch;
    private bool breakerOff = false;
    private bool canUseFuseBox = false;


    //Open closet
    public MeshRenderer bathroomLightMesh;
    public MeshRenderer brokenWindowMesh;
    private Material bathroomLightMat;
    private Material brokenWindowMat;
    public Light bathroomLight;
    public GameObject closetDoor;
    private bool closetTriggerActive = true;


    private bool canOpenDungeonDoor = false;
    private bool canUseSnackMachine = false;
    private bool canUseLockedDoor = false;
    private bool playerSafe = false;
    private string paperInstructionString = "Press [Esc] to view objectives";
    public enum TriggerType {Darkness, Maze, Paper, Light, FuseBox, AudiOnly, DungeonDoor, LockedDoor,
                                Closet, Safety}
    public TriggerType triggerType;
    void Awake()
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
        if(triggerType == TriggerType.Light || triggerType == TriggerType.FuseBox)
        {
            //Create array of materials from all used lights
            for(int i = 0; i < lightFoundations.Length; i++)
            {
                lightFoundationMaterials[i] = lightFoundations[i].material;
            }
            //Set reference to normal color of emission map, for turning lights on
            normalEmissionColor = lightFoundationMaterials[0].color;
        }
        if(triggerType == TriggerType.Closet)
        {
            brokenWindowMat = brokenWindowMesh.material;
            bathroomLightMat = bathroomLightMesh.material;
        }
    }
    void Update()
    {
        if(playerSafe)
        {
            Debug.Log("Player safe!");
        }
        if(canGrabPaper)
        {
            //If object material is currently highlighted, means player is looking at it
            // Check for that before E input
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
        if(canUseSnackMachine)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteAudioOnlyTrigger();
            }
        }
        if(canUseLightSwitch)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteLightSwitchTrigger();
            }
        }
        if(canUseLockedDoor)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteAudioOnlyTrigger();
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
            if(gameObject.tag == "DarknessTrigger")
            {
                if(darknessTriggerActive)
                {
                    ExecuteDarknessTrigger();
                }
            }
            else if(gameObject.tag == "ClosetTrigger")
            {
                if(closetTriggerActive)
                {
                    ExecuteClosetTrigger();
                }
            }
            else if(gameObject.tag == "MainPaperTrigger")
            {
                canGrabPaper = true;
            }
            else if(gameObject.tag == "DungeonTrigger")
            {
                canOpenDungeonDoor = true;
            }
            else if(gameObject.tag == "SnackTrigger")
            {
                canUseSnackMachine = true;
            }
            else if(gameObject.tag == "LightSwitchTrigger")
            {
                canUseLightSwitch = true;
            }
            else if(gameObject.tag == "DoorLockedTrigger")
            {
                canUseLockedDoor = true;
            }
            else if(gameObject.tag == "FuseBoxTrigger")
            {
                canUseFuseBox = true;
            }
            else if(gameObject.tag == "SafetyTrigger")
            {
                playerSafe = true;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gameObject.tag == "MainPaperTrigger")
            {
                canGrabPaper = false;
            }
            else if(gameObject.tag == "DungeonTrigger")
            {
                canOpenDungeonDoor = false;
            }
            else if(gameObject.tag == "SnackTrigger")
            {
                canUseSnackMachine = false;
            }
            else if(gameObject.tag == "LightSwitchTrigger")
            {
                canUseLightSwitch = false;
            }
            else if(gameObject.tag == "DoorLockedTrigger")
            {
                canUseLockedDoor = false;
            }
            else if(gameObject.tag == "FuseBoxTrigger")
            {
                canUseFuseBox = false;
            }
            else if(gameObject.tag == "SafetyTrigger")
            {
                playerSafe = false;
            }
        }
    }
    //Trigger for main paper in head office
    public void ExecuteMainPaperTrigger()
    {
        triggerAudio.Play();
        gameController.currentCheckpoint = 1;
        StartCoroutine(gameController.ShowPopupMessage(paperInstructionString, 2));
        //mainPaper.SetActive(false);
    }
    //Trigger to enter final dungeon scene
    public void ExecuteDungeonTrigger()
    {
        triggerAudio.Play();
        levelController.FadeInToLevel(2);
        //StartCoroutine(DelayFadeToLevel(2f, 2));
    }
    public void ExecuteAudioOnlyTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
    }
    public void ExecuteLightSwitchTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        //Turn lights on
        if(lightsOff)
        {
            lightsOff = false;
            //Rotate lightswitch to "on"
            lightSwitch.transform.eulerAngles = new Vector3(-60, 0, 0);
            foreach(Light light in controlledLights)
            {
                light.enabled = true;
            }
            //Enable emission maps when lights turned on
            foreach(Material mat in lightFoundationMaterials)
            {
                mat.SetColor("_EmissionColor", normalEmissionColor);
            }
        }
        //Turn lights off
        else
        {
            lightsOff = true;
            //Rotate lightswitch to "off"
            lightSwitch.transform.eulerAngles = new Vector3(-20, 0, 0);
            foreach(Light light in controlledLights)
            {
                light.enabled = false;
            }
            //Disable emission maps when lights turned off
            foreach(Material mat in lightFoundationMaterials)
            {
                mat.SetColor("_EmissionColor", Color.black);
            }
        }
    }
    public void ExecuteFuseBoxTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        //Turn lights on
        if(breakerOff)
        {
            breakerOff = false;
            //Switch breaker "on"
            fuseSwitch.transform.eulerAngles = new Vector3(-60, 0, 0);
            foreach(Light light in breakerLights)
            {
                light.enabled = true;
            }
            //Enable emission maps when lights turned on
            foreach(Material mat in lightFoundationMaterials)
            {
                mat.SetColor("_EmissionColor", normalEmissionColor);
            }
        }
        //Turn lights off
        else
        {
            breakerOff = true;
            //Switch breaker "off"
            fuseSwitch.transform.eulerAngles = new Vector3(-20, 0, 0);
            foreach(Light light in breakerLights)
            {
                light.enabled = false;
            }
            //Disable emission maps when lights turned off
            foreach(Material mat in lightFoundationMaterials)
            {
                mat.SetColor("_EmissionColor", Color.black);
            }
        }
    }
    //Single use, deactivate after
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
        darknessTriggerActive = false;
    }
    //Single use, deactivate after
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
        closetTriggerActive = false;
    }
    IEnumerator DelayFadeToLevel(float delayTime, int levelNumber)
    {
        yield return new WaitForSeconds(delayTime);
        levelController.FadeInToLevel(levelNumber);
    }
}
