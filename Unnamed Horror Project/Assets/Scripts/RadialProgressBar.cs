using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private float timer = 0.01f;
    [SerializeField] private float maxTimer = 5.0f;
    [SerializeField] private Image radialImage;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource radialDoneAudioSource;


    [Header("Sink")]
    [SerializeField] private AudioSource sinkScareAudioSource;
    [SerializeField] private AudioSource sinkDripAudioSource;
    [SerializeField] private AudioSource ratchetAudioSource;
    [SerializeField] private GameObject waterDripEffect;
    public bool canUpdate = false;
    private bool canInteract = false;
    //put script on empty object that is where you want player to interact
    // reference ui object here instead of having this script on ui object
    //  if distance between object and player is small enough, canInteract
    private float distance;
    public enum RadialType{Window, Sink, TV, BloodChalice};
    public RadialType radialType;
    void Update()
    {
        if(radialType == RadialType.Sink)
        {
            /*
            if(canUpdate)
            {
                radialImage.enabled = true;
                UpdateRadialFill();
            }
            else
            {
                radialImage.enabled = false;
            }
            */
        }
        /*
        if(canInteract)
        {
            radialImage.enabled = true;
            if(radialType == RadialType.Sink)
            {
                if(gameController.currentCheckpoint == 4)
                {
                    UpdateRadialFill();
                }
            }
            else if(radialType == RadialType.Window)
            {
                if(gameController.currentCheckpoint == 2)
                {
                    UpdateRadialFill();
                }
            }
            else if(radialType == RadialType.TV)
            {
                if(gameController.currentCheckpoint == 6)
                {
                    UpdateRadialFill();
                }
            }
        }
        */
    }
    private void UpdateRadialFill()
    {
        if(Input.GetKey(KeyCode.E))
        {
            timer += Time.deltaTime / 4;
            radialImage.fillAmount = timer;

            if(radialType == RadialType.Sink)
            {
                if(!ratchetAudioSource.isPlaying)
                {
                    ratchetAudioSource.Play();
                }
            }
            if(radialType == RadialType.Window)
            {
                
            }
            if(radialType == RadialType.TV)
            {
                
            }

            if(timer == maxTimer / 3)
            {
                if(radialType == RadialType.Sink)
                {
                    sinkScareAudioSource.Play();
                    Debug.Log("spooked");
                }
                if(radialType == RadialType.Window)
                {
                    
                }
                if(radialType == RadialType.TV)
                {
                    
                }
            }

            if(timer >= maxTimer)
            {
                timer = 0.01f;
                radialImage.fillAmount = 0.01f;
                radialImage.enabled = false;
                canUpdate = false;
                radialDoneAudioSource.Play();

                if(radialType == RadialType.Sink)
                {
                    //Disable sink VFX + SFX
                    sinkDripAudioSource.Stop();
                    waterDripEffect.SetActive(false);
                    ratchetAudioSource.Stop();

                    gameController.currentCheckpoint = 5;

                }
                if(radialType == RadialType.Window)
                {
                    //play done sfx? small ding or something
                    gameController.currentCheckpoint = 3;
                }
                if(radialType == RadialType.TV)
                {
                    //play done sfx? small ding or something
                    gameController.currentCheckpoint = 7;
                }
            }
        }
        else
        {
            if(radialType == RadialType.Sink)
            {
                ratchetAudioSource.Pause();
            }
            if(radialType == RadialType.Window)
            {
                
            }
            if(radialType == RadialType.TV)
            {
                
            }
        }
    }
}
