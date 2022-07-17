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

    private string cantLeaveYetString = "I should see what the job's about first...";
    private string betterOffHereString = "I drove all the way here might as well make some money";
    
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void AttemptToUseCar()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if(gameController.currentCheckpoint <= 1)
        {
            StartCoroutine(gameController.ShowPopupMessage(cantLeaveYetString, 2));
        }
        else if(gameController.currentCheckpoint == 7)
        {
            StartSensibleCarEnding();
        }
        else
        {
            StartCoroutine(gameController.ShowPopupMessage(betterOffHereString, 2));  
        }
    }
    
    //Called by choosing "Yes" in Car Ending UI screen
    public void StartSensibleCarEnding()
    {
        audioSource.clip = carEndingAudioClip;
        audioSource.Play();
        Time.timeScale = 1;
        levelController.LoadLevel(2);
    }
}
