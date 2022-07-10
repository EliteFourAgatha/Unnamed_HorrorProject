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
    [SerializeField] private AudioClip collectedSFX;

    [Header("ToolBox")]
    [SerializeField] private GameObject toolBox;
    private string toolBoxString = "'Tighten the nuts on the bathroom sink and stop the leak'";
    
    [Header("PickUpBox")]
    [SerializeField] private GameObject pickUpBox;
    [SerializeField] private GameObject heldCardboardBox;
    [SerializeField] private GameObject cardboardBoxOnTable;
    [SerializeField] private GameObject blownFuseTrigger;
    [SerializeField] private GameObject shadowBlurTrigger;
    [SerializeField] private GameObject coffeeTableObject;    
    private bool blownFuseTriggerActive = true;
    private string cardboardBoxString = "'Put the box in the head office'";
    private string coffeeTableString = "Find an extra fuse for the fuse box";

    [Header("Fuse")]
    [SerializeField] private GameObject fuse;
    private string useFuseString = "Restore power to the basement";



    [Header("MainPaper")]
    [SerializeField] private GameObject mainPaper;    
    [SerializeField] private AudioClip mainPaperSFX;
    private string mainPaperString = "'Find the tools I left for you in the basement'";


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
    private string hideString = "Hide!";

    private string keyString = "There's a small key hidden in the book.";
    private string fuseboxString = "'Nail the laundry window shut until I can find a better solution'";

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
                if(blownFuseTriggerActive)
                {
                    TriggerBlownFuse();
                }
            }
            //Shadow scare after fixing light
            else if(triggerType == TriggerType.ShadowScare)
            {
                if(shadowTriggerActive)
                {
                    TriggerBlurMotionScare();
                }
            }
        }
    }
    public void PickUpMainPaper()
    {
        StartCoroutine(PickUpKeyItem(1, mainPaper, mainPaperSFX, mainPaperString));
    }    
    public void PickUpToolBox()
    {
        gameController.playerHasToolBox = true;
        StartCoroutine(PickUpKeyItem(2, toolBox, collectedSFX, toolBoxString));
    }
    public void PickUpCardboardBox()
    {
        gameController.playerHasPickUpBox = true;
        StartCoroutine(PickUpKeyItem(5, pickUpBox, collectedSFX, cardboardBoxString));
        heldCardboardBox.SetActive(true);
        blownFuseTrigger.SetActive(true);        
    }
    public void PickUpFuse()
    {
        gameController.playerHasFuse = true;
        StartCoroutine(PickUpKeyItem(7, fuse, collectedSFX, useFuseString));
    }

    //Coffee table where you place box
    public void InteractWithCoffeeTable()
    {
        gameController.currentCheckpoint = 6;
        StartCoroutine(gameController.ShowPopupMessage(coffeeTableString, 2));
        heldCardboardBox.SetActive(false);
        cardboardBoxOnTable.SetActive(true);
        coffeeTableObject.tag = "Untagged";
    }

    public void InteractWithFuseBox()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        gameController.currentCheckpoint = 8;
        StartCoroutine(gameController.ShowPopupMessage(fuseboxString, 2));
        gameController.breakerOn = true;
        foreach(Light light in breakerLights)
        {
            light.enabled = true;
        }
    }


    public void TriggerBlurMotionScare()
    {
        musicAudioSource.Stop();
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        shadowBlurMonster.SetActive(true);

        if(!heartBeatAudioSource.isPlaying)
        {
            heartBeatAudioSource.Play();
        }

        shadowTriggerActive = false; // single use, deactivate after
    }

    public void TriggerBlownFuse()
    {
        //Disable all lights? Or leave bathroom/ laundry?
        foreach(Light light in breakerLights)
        {
            light.enabled = false;
        }
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        shadowBlurTrigger.SetActive(true);

        blownFuseTriggerActive = false; // single use, deactivate after

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
   
    IEnumerator PickUpKeyItem(int nextCheckpoint, GameObject disabledObj, AudioClip objectSFX, string textMessage)
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
        StartCoroutine(gameController.ShowPopupMessage(hideString, 2f));
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