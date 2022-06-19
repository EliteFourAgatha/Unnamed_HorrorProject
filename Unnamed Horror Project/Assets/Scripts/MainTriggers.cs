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

    [Header("Collectibles")]
    [SerializeField] private GameObject toolBox;
    [SerializeField] private GameObject fuse;
    [SerializeField] private AudioClip collectedSFX;
    [SerializeField] private GameObject mainPaper;    
    [SerializeField] private AudioClip mainPaperSFX;  


    [Header("Fuse Box")]
    [SerializeField] private Light[] breakerLights;

    [Header("First Scare")]
    [SerializeField] private GameObject shadowBlurMonster;
    private bool shadowTriggerActive = true;
    [SerializeField] private AudioSource heartBeatAudioSource;



    [Header("Final Closet")]
    [SerializeField] private GameObject[] disabledObjects;
    [SerializeField] private GameObject sewerMasterObject;
    [SerializeField] private GameObject aiMonster;
    [SerializeField] private GameObject aiMonsterTwo;
    [SerializeField] private GameObject aiMonsterThree;
    [SerializeField] private Animator closetDoorAnim;
    private bool closetTriggerActive = true;
    [SerializeField] private Light backroomLightOne;
    [SerializeField] private AudioSource lightOneAudioSource;
    [SerializeField] private Light backroomLightTwo;
    [SerializeField] private AudioSource lightTwoAudioSource;
    [SerializeField] private AudioSource closetCreakAudioSource;
    [SerializeField] private AudioClip closetCreakAudio;
    [SerializeField] private GameObject topOfStairsDoor;

    private string keyString = "There's a small key hidden in the book";

    private string paperInstructionString = "Press [Esc] to view objectives";
    //private string findFuseString = "Find the fuse for the fuse box in the basement.";
    private string useFuseString = "Restore power to the basement";
    private string toolBoxString = "Nail the laundry window shut";
    //private string BathroomToFuseBoxString = "Turn off breaker to storage area";
    private string fuseboxString = "Find the tools left for you in the basement";

    [SerializeField] private enum TriggerType {Escape, Closet, ShadowScare, BlownFuse}
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
            //Before blur scare, lights go out
            else if(triggerType == TriggerType.BlownFuse)
            {
                if(shadowTriggerActive)
                {
                    TriggerBlownFuse();
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
    
    public void PickUpToolBox()
    {
        gameController.currentCheckpoint = 4;
        gameController.playerHasToolBox = true;
        StartCoroutine(WaitAndDisableObject(2, toolBox, collectedSFX, toolBoxString));
    }

    public void PickUpFuse()
    {
        gameController.currentCheckpoint = 2;
        gameController.playerHasFuse = true;
        StartCoroutine(WaitAndDisableObject(2, fuse, collectedSFX, useFuseString));
    }

    public void InteractWithFuseBox()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        gameController.currentCheckpoint = 3;
        StartCoroutine(gameController.ShowPopupMessage(fuseboxString, 2));
        gameController.breakerOn = true;
        foreach(Light light in breakerLights)
        {
            light.enabled = true;
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

        shadowTriggerActive = false; // single use, deactivate after
    }

    public void TriggerBlownFuse()
    {
        //lights go out (all except laundry + bathroom? or all?)
        //ominous sfx or rumble
        //blownFuseTriggerActive = false;
    }

    public void TriggerFoundKey()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        StartCoroutine(gameController.ShowPopupMessage(keyString, 3f));
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
    public void TriggerFinalCloset()
    {        
        //Enable door at top of stairs
        //  --Forces them to hide / confront the monster--
        topOfStairsDoor.SetActive(true);

        sewerMasterObject.SetActive(true);


        //Disable exterior objects that clip with sewer area
        foreach(GameObject obj in disabledObjects)
        {
            obj.SetActive(false);
        }
        StartCoroutine(OpenClosetAfterDelay(2f));

        gameController.currentCheckpoint = 8;
        
        closetTriggerActive = false; //single use, deactivate after
    }
    
    // Slowly creak door open + sound effect
    IEnumerator OpenClosetAfterDelay(float delay)
    {
        backroomLightOne.enabled = false;
        lightOneAudioSource.Play();
        backroomLightTwo.enabled = false;
        lightTwoAudioSource.Play();
        yield return new WaitForSeconds(delay);
        closetCreakAudioSource.clip = closetCreakAudio;
        closetCreakAudioSource.Play();
        closetDoorAnim.Play("ClosetCreakOpen");
        yield return new WaitForSeconds(2f);
        aiMonster.SetActive(true);
        aiMonsterTwo.SetActive(true);
        aiMonsterThree.SetActive(true);
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
        levelController.LoadLevel(3);
    }
}