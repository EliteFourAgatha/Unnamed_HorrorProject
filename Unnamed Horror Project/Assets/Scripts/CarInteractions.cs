using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarInteractions : MonoBehaviour
{
    private AudioSource audioSource;
    public LevelController levelController;
    private GameController gameController;
    public GameObject carEndingUI;
    private bool canDoCarEnding;
    private bool canInteractWithCar;
    private string batteryDeadString = "The battery's dead... This can't be happening..";
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if(gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        if(gameController == null)
        {
            levelController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        }
    }
    private void Start()
    {
        canInteractWithCar = false;
        //Disable car ending at start
        canDoCarEnding = false;
    }
    public void Update()
    {
        if(canDoCarEnding)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Enable popup UI (semi-transparent, freezes game behind it)
                // "Are you sure you want to quit/exit now?"

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
                StartCoroutine(gameController.ShowMessage(batteryDeadString, 2));
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
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
        //
        //Keep track of car ending having been found (2/4 endings found)
        //
        //
        //Fade in to "game over screen / stats / endings" screen
        levelController.FadeInToLevel(0);
    }
}
