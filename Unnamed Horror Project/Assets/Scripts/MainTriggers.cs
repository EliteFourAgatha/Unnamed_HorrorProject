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
    
    [Header("Pliers")]
    public GameObject pliers;    
    public AudioClip pliersSFX;

    [Header("Fuse Box")]
    public Light[] breakerLights;


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
    public GameObject aiMonster;
    public GameObject lockedClosetDoorTrigger;
    public Animator closetDoorAnim;
    private bool closetTriggerActive = true;
    public Light backroomLightOne;
    public AudioSource lightOneAudioSource;
    public Light backroomLightTwo;
    public AudioSource lightTwoAudioSource;
    public AudioSource closetCreakAudioSource;
    public AudioClip closetCreakAudio;


    private string paperInstructionString = "Press [Esc] to view objectives";
    private string findFuseString = "Find the fuse for the fuse box in the basement.";
    private string findToolsString = "Find the wrench left for you in storage";
    private string fixLaundryWindowString = "Nail the laundry window shut";
    private string pliersString = "Fix bathroom sink leak";
    private string BathroomToFuseBoxString = "Turn off breaker to storage area";
    private string fuseboxString = "Find the pliers left for you in the basement.";

    public enum TriggerType {Escape, Closet, ShadowScare}
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
            if(triggerType == TriggerType.Closet)
            {
                if(closetTriggerActive)
                {
                    TriggerFinalCloset();
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
        gameController.currentCheckpoint = 1;
        StartCoroutine(WaitAndDisableObject(1, mainPaper, mainPaperSFX, paperInstructionString));
    }
    public void PickUpPliers() // Pliers in basement
    {
        if(gameController.currentCheckpoint == 2)
        {
            gameController.currentCheckpoint = 3;
            gameController.playerHasPliers = true;
            StartCoroutine(WaitAndDisableObject(2, pliers, pliersSFX, pliersString));
        }
    }
    public void TriggerFuseBox()
    {
        //Instead of checking for a checkpoint (makes no sense logically from player pov)
        // should check to see if player has fuse or fuse has been inserted etc.
        if(gameController.currentCheckpoint == 1)
        {
            if(!triggerAudio.isPlaying)
            {
                triggerAudio.Play();
            }
            gameController.currentCheckpoint = 2;
            StartCoroutine(gameController.ShowPopupMessage(fuseboxString, 2));
            gameController.breakerOn = true;
            foreach(Light light in breakerLights)
            {
                light.enabled = true;
            }
        }
    }
    // Trigger in final closet that is win condition for player
    // If player looking at ladder and presses Interact...
    public void TriggerEscape()
    {
        StartCoroutine(EnableEscapeCutscene());
    }
    IEnumerator EnableEscapeCutscene()
    {
        levelController.FadeToBlack();
        yield return new WaitForSeconds(1f);
        levelController.LoadLevel(2);
    }
    //Triggered by player near end of game in basement
    // Lights go out, door creaks open
    //  Monster appears and begins to chase player
    public void TriggerFinalCloset()
    {        
        //Enable door at top of stairs
        // Door fades in as player gets closer, can't escape that way
        //  Forces them to hide / confront the monster
        topOfStairsDoor.SetActive(true);
        StartCoroutine(ShatterLightsBeforeCloset());
        aiMonster.SetActive(true);
        StartCoroutine(OpenClosetAfterDelay(2f));
        //after closet opens, cutscene?
        // or just set monster chase mode active here, instead of automatically
        //  when you first spawn it (would be running into door)
        lockedClosetDoorTrigger.SetActive(false); //Trigger on closet door        

        closetTriggerActive = false; //single use, deactivate after
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



    //Consider moving this to new script. Exposition script (not a main trigger)
    public void ExecuteExpositionNote()
    {
        
    }


    
    IEnumerator WaitAndDisableObject(int nextCheckpoint, GameObject disabledObj, AudioClip objectSFX, string textMessage)
    {
        AudioSource.PlayClipAtPoint(objectSFX, gameObject.transform.position);
        gameController.currentCheckpoint = nextCheckpoint;
        StartCoroutine(gameController.ShowPopupMessage(textMessage, 1f));
        yield return new WaitForSeconds(1f);
        disabledObj.SetActive(false);
    }
    //Closet trigger part 1
    // Turn off lights in back room of basement
    IEnumerator ShatterLightsBeforeCloset()
    {
        backroomLightOne.enabled = false;
        lightOneAudioSource.Play();
        yield return new WaitForSeconds(1f);
        backroomLightTwo.enabled = false;
        lightTwoAudioSource.Play();
    }
    //Closet trigger part 2
    // Slowly creak door open + sound effect
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