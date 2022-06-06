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
    private bool scaredEndingPossible = false;
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
            scaredEndingPossible = true;
        }
        else
        {
            scaredEndingPossible = false;
        }
        if(gameController.currentCheckpoint == 4 || gameController.currentCheckpoint == 5)
        {
            sensibleEndingPossible = true;
        }
        else
        {
            sensibleEndingPossible = false;
        }
        Debug.Log("ATTEMPTING TO USE CAR");
        if(scaredEndingPossible)
        {
            StartCarEnding(2);
        }
        else if(sensibleEndingPossible)
        {
            StartCarEnding(3);
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
    public void StartCarEnding(int endingNumber)
    {
        audioSource.clip = carEndingAudioClip;
        audioSource.Play();
        carEndingUI.SetActive(false);
        Time.timeScale = 1;
        if(endingNumber == 2)
        {
            levelController.LoadLevel(2);
        }
        else if(endingNumber == 3)
        {
            levelController.LoadLevel(3);
        }
        else if(endingNumber == 4)
        {
            levelController.LoadLevel(4);
        }

    }
}
