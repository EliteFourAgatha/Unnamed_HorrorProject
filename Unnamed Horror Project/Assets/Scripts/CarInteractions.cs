using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarInteractions : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip carEndingAudioClip;
    [SerializeField] private LevelController levelController;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject carEndingUI;
    //The car ending being possible. Available after checkpoint X (first scare)
    private bool sensibleEndingPossible = false;
    //private bool scaredEndingPossible = false;
    private string cantLeaveYetString = "I should see what the job's about first...";
    private string whyLeaveEasyMoneyString = "What's the rush? Easy money to be made";
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void AttemptToUseCar()
    {
        if(gameController.currentCheckpoint == 2)
        {
            //"having barely seen anything / startled by a bump in the night, our
            //   protagonist flees with his tail between his legs."

            //scaredEndingPossible = true;
        }
        else if(gameController.currentCheckpoint == 5)
        {
            //"considering the unearthly sounds emanating from the basement, 
            //  our protagonist makes the sensible decision to leave and consider other employment."
            sensibleEndingPossible = true;
        }
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
        levelController.LoadLevel(0);
    }
}
