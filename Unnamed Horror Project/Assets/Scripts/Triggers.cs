using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour
{
    public GameController gameController;
    public LevelController levelController;
    public GameObject darknessWall;
    public GameObject closetLightBulb;
    public GameObject fakeCloset;
    public GameObject dungeonDoor;
    public GameObject mainPaper;
    public Image mainPaperImage;
    public Sprite mainPaperSprite;
    private bool canGrabPaper = false;
    private bool canOpenDungeonDoor = false;
    public AudioSource triggerAudio;

    private string paperInstructionString = "Press [Esc] to view objectives";

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
    }
    void Start()
    {
        canGrabPaper = false;
    }
    void Update()
    {
        if(canGrabPaper)
        {
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
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gameObject.tag == "DarknessTrigger")
            {
                ExecuteDarknessTrigger();
            }
            else if(gameObject.tag == "MainPaperTrigger")
            {
                canGrabPaper = true;
            }
            else if(gameObject.tag == "DungeonTrigger")
            {
                canOpenDungeonDoor = true;
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
        }
    }
    //If player enters darkness trigger, 4 things happen
    //1. darkness wall spawns behind player so they can't leave
    //2. light bulb light goes out
    //3. loud / spooky noise or ominouse tone to show that shit is going down


    //4. dungeon door spawns and fades into view (Or player can do reverse alpha fade
    // as they get closer to door?)
    public void ExecuteDarknessTrigger()
    {
        darknessWall.SetActive(true);
        closetLightBulb.SetActive(false);
        fakeCloset.SetActive(false);
        dungeonDoor.SetActive(true);
        Debug.Log("Darkness activated!");
    }
    //Trigger for main paper in head office
    public void ExecuteMainPaperTrigger()
    {
        mainPaperImage.sprite = mainPaperSprite;
        mainPaperImage.enabled = true;
        gameController.currentCheckpoint = 1;
        gameController.ShowPopupMessage(paperInstructionString, 2);
        mainPaper.SetActive(false);
    }
    //Trigger to enter final dungeon scene
    public void ExecuteDungeonTrigger()
    {
        triggerAudio.Play();
        levelController.FadeInToLevel(2);
        //StartCoroutine(DelayFadeToLevel(2f, 2));
    }
    IEnumerator DelayFadeToLevel(float delayTime, int levelNumber)
    {
        yield return new WaitForSeconds(delayTime);
        levelController.FadeInToLevel(levelNumber);
    }
}
