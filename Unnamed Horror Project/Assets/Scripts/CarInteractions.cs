using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
//
//Put into trigger script??
//
public class CarInteractions : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip carEndingAudioClip;
    public LevelController levelController;
    private GameController gameController;
    public GameObject carEndingUI;
    //The car ending still being possible. Available after checkpoint 1 (first scare)
    //  but must be done before checkpoint X (xxxxx)
    private bool carEndingPossible;
    private bool canInteractWithCar;
    private string cantLeaveYetString = "I should see what the job's about first...";
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if(gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        if(levelController == null)
        {
            levelController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        }
    }
    private void Start()
    {
        canInteractWithCar = false;
        //Disable car ending at start
        carEndingPossible = false;
    }
    public void Update()
    {
        if(gameController.currentCheckpoint == 2)
        {
            carEndingPossible = true;
            audioSource.clip = carEndingAudioClip;
        }
        else
        {
            carEndingPossible = false;
        }
        if(canInteractWithCar)
        {
            //If car ending possible (between checkpoint 1 and X)
            if(carEndingPossible)
            {
                //Prompt to ensure player wants to quit by car
                if(Input.GetKeyDown(KeyCode.E))
                {
                    carEndingUI.SetActive(true);
                    Time.timeScale = 0;
                    //cursor.lockState = CursorLockMode.Locked;
                    //cursor.visible = false;
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                    StartCoroutine(gameController.ShowPopupMessage(cantLeaveYetString, 2));
                }
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canInteractWithCar = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canInteractWithCar = false;
        }
    }
    //Ending 1. Available from very start of game from first
    //  eerie SFX/music/long shot of dark corridor you have to go down.
    //   No longer available after xxxx. You go outside and find the tires have been slashed.
    //     Or, even better, you realize you left the car battery on and now it's dead.
    //       (Car headlights on when you go inside building, off when you go out next/hit first checkpoint)
    //     "This can't be happening...."

    //  "No job is worth this shit"

    //Called by choosing "Yes" in Car Ending UI screen
    public void StartCarEnding()
    {
        audioSource.Play();
        carEndingUI.SetActive(false);
        Time.timeScale = 1;
        //
        //Keep track of car ending having been found (2/4 endings found)
        //
        //
        //Fade in to "game over screen / stats / endings" screen        
        levelController.FadeInToLevel(3);
    }
}
