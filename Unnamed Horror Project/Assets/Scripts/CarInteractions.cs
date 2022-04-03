using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarInteractions : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip carEndingAudioClip;
    public LevelController levelController;
    private GameController gameController;
    public GameObject carEndingUI;
    //The car ending being possible. Available after checkpoint X (first scare)
    private bool carEndingPossible = false;
    private string cantLeaveYetString = "I should see what the job's about first...";
    private string whyLeaveEasyMoneyString = "What's the rush? Easy money to be made";
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
    public void Update()
    {
        if(gameController.currentCheckpoint >= 2)
        {
            carEndingPossible = true;
        }
        else
        {
            carEndingPossible = false;
        }
    }
    public void AttemptToUseCar()
    {
        if(carEndingPossible)
        {
            StartCarEnding();
        }
        else
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if(gameController.currentCheckpoint <= 1)
            {
                StartCoroutine(gameController.ShowPopupMessage(cantLeaveYetString, 2));
            }
            else
            {
                StartCoroutine(gameController.ShowPopupMessage(whyLeaveEasyMoneyString, 2));                
            }
        }
    }
    //Called by choosing "Yes" in Car Ending UI screen
    public void StartCarEnding()
    {
        audioSource.clip = carEndingAudioClip;
        audioSource.Play();
        carEndingUI.SetActive(false);
        Time.timeScale = 1;
        //
        //Keep track of car ending having been found (2/4 endings found)
        //
        //
        //Fade in to "game over screen / stats / endings" screen
        // "No job is worth this shit" ending        
        levelController.FadeInToLevel(1);
    }
}
