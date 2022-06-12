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

    private bool sensibleEndingPossible = false;
    private string cantLeaveYetString = "I should see what the job's about first...";
    private string betterOffHereString = "I'd rather stay and make money than drive home in this rain...";
    
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void AttemptToUseCar()
    {
        if(gameController.currentCheckpoint == 4 || gameController.currentCheckpoint == 5)
        {
            sensibleEndingPossible = true;
        }
        else
        {
            sensibleEndingPossible = false;
        }
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
                StartCoroutine(gameController.ShowPopupMessage(betterOffHereString, 2));                
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
        levelController.LoadLevel(2);
    }
}
