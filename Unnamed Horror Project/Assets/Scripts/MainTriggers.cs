using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Consider creating scare trigger script and one for simple item interactions.

public class MainTriggers : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private LevelController levelController;
    [SerializeField] private AudioSource triggerAudio;
    [SerializeField] private AudioSource musicAudioSource;


    [Header("Main Paper")]
    [SerializeField] private GameObject mainPaper;    
    [SerializeField] private AudioClip mainPaperSFX;
    
    [Header("Pliers")]
    [SerializeField] private GameObject pliers;    
    [SerializeField] private AudioClip pliersSFX;

    [Header("Fuse Box")]
    [SerializeField] private Light[] breakerLights;


    [Header("Head Office Key")]
    [SerializeField] private GameObject heldKey;
    [SerializeField] private GameObject heldKeyUI;


    [Header("First Scare")]
    [SerializeField] private GameObject shadowBlurMonster;
    private bool shadowTriggerActive = true;
    [SerializeField] private AudioSource heartBeatAudioSource;
    [SerializeField] private AudioSource gurglingAudioSource;



    [Header("Final Closet")]
    [SerializeField] private GameObject[] disabledObjects;
    [SerializeField] private GameObject sewerMasterObject;
    [SerializeField] private GameObject aiMonster;
    [SerializeField] private GameObject lockedClosetDoorTrigger;
    [SerializeField] private Animator closetDoorAnim;
    private bool closetTriggerActive = true;
    [SerializeField] private Light backroomLightOne;
    [SerializeField] private AudioSource lightOneAudioSource;
    [SerializeField] private Light backroomLightTwo;
    [SerializeField] private AudioSource lightTwoAudioSource;
    [SerializeField] private AudioSource closetCreakAudioSource;
    [SerializeField] private AudioClip closetCreakAudio;
    [SerializeField] private GameObject topOfStairsDoor;


    private string paperInstructionString = "Press [Esc] to view objectives";
    //private string findFuseString = "Find the fuse for the fuse box in the basement.";
    //private string findToolsString = "Find the wrench left for you in storage";
    //private string fixLaundryWindowString = "Nail the laundry window shut";
    private string pliersString = "Fix bathroom sink leak";
    //private string BathroomToFuseBoxString = "Turn off breaker to storage area";
    private string fuseboxString = "Find the pliers left for you in the basement.";

    [SerializeField] private enum TriggerType {Escape, Closet, ShadowScare}
    [SerializeField] private TriggerType triggerType;
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


    public void TriggerFirstScare()
    {
        musicAudioSource.Stop();
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        shadowBlurMonster.SetActive(true);

        //Remember to turn this off somewhere. Loses effect the longer it stays on.
        //  Should be for tense moments, then fade out
        if(!heartBeatAudioSource.isPlaying)
        {
            heartBeatAudioSource.Play();
        }
        if(!gurglingAudioSource.isPlaying)
        {
            gurglingAudioSource.Play();
        }
        shadowTriggerActive = false; // single use, deactivate after
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
   
    IEnumerator WaitAndDisableObject(int nextCheckpoint, GameObject disabledObj, AudioClip objectSFX, string textMessage)
    {
        AudioSource.PlayClipAtPoint(objectSFX, gameObject.transform.position);
        gameController.currentCheckpoint = nextCheckpoint;
        StartCoroutine(gameController.ShowPopupMessage(textMessage, 1f));
        yield return new WaitForSeconds(1f);
        disabledObj.SetActive(false);
    }

    //Triggered by player near end of game in basement
    // Lights go out, door creaks open
    //  Monster appears and begins to chase player
    public void TriggerFinalCloset()
    {        
        //Enable door at top of stairs
        //  --Forces them to hide / confront the monster--
        topOfStairsDoor.SetActive(true);

        sewerMasterObject.SetActive(true);
        aiMonster.SetActive(true);

        //Disable exterior objects that clip with sewer area
        foreach(GameObject obj in disabledObjects)
        {
            obj.SetActive(false);
        }

        StartCoroutine(ShatterLightsBeforeCloset());
        StartCoroutine(OpenClosetAfterDelay(2f));

        lockedClosetDoorTrigger.SetActive(false); //Trigger on closet door        

        closetTriggerActive = false; //single use, deactivate after
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
    // Trigger on ladder in sewer, win condition
    public void TriggerEscape()
    {
        StartCoroutine(EnableEscapeCutscene());
    }
    IEnumerator EnableEscapeCutscene()
    {
        levelController.FadeToBlack();
        yield return new WaitForSeconds(1f);
        levelController.LoadLevel(0);
    }
}