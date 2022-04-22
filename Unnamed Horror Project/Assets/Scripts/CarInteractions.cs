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
    private bool sensibleEndingPossible = false;
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
        if(gameController.currentCheckpoint == 2)
        {
            //"having barely seen anything / startled by a bump in the night, our
            //   protagonist flees with his tail between his legs."
            scaredyCatEnding = true;
        }
        else if(gameController.currentCheckpoint == 5)
        {
            //"considering the unearthly sounds emanating from the basement, 
            //  our protagonist makes the sensible decision to leave and consider other employment."
            sensibleEndingPossible = true;
        }
    }
    public void AttemptToUseCar()
    {
        Debug.Log("CARCARCAR");
        if(sensibleEndingPossible)
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
