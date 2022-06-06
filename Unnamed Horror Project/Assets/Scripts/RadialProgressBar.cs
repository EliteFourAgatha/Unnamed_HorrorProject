using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private float timer = 0.01f;
    [SerializeField] private float maxTimer = 5.0f;
    public Image radialImage;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource radialDoneAudioSource;
    [SerializeField] private AudioSource toolAudioSource;
    public AudioClip ratchetClip;
    public AudioClip hammerClip;


    [Header("Sink")]
    [SerializeField] private AudioSource sinkScareAudioSource;
    [SerializeField] private AudioSource sinkDripAudioSource;
    [SerializeField] private GameObject waterDripEffect;
    [SerializeField] private GameObject sinkObj;
    public bool canUpdateSink = false;
    public bool canUpdateWindow = false;
    public bool canUpdateTV = false;
    private bool canInteract = false;

    [Header("Window + TV")]
    [SerializeField] private GameObject tvObj;
    [SerializeField] private GameObject windowObj;
    //put script on empty object that is where you want player to interact
    // reference ui object here instead of having this script on ui object
    //  if distance between object and player is small enough, canInteract
    public enum RadialType {TV, Window, Sink};
    public RadialType radialType;
    private float distance;
    void Update()
    {
        if(canUpdateSink)
        {
            radialImage.enabled = true;
            if(Input.GetKey(KeyCode.E))
            {
                UpdateRadialFill();
            }
        }
        else if(canUpdateWindow)
        {
            radialImage.enabled = true;
            if(Input.GetKey(KeyCode.E))
            {
                UpdateRadialFill();
            }
        }
        else if(canUpdateTV)
        {
            radialImage.enabled = true;
            if(Input.GetKey(KeyCode.E))
            {
                UpdateRadialFill();
            }
        }
        else
        {
            //radialImage.enabled = false;
        }
    }
    public void UpdateRadialFill()
    {
        timer += Time.deltaTime;
        radialImage.fillAmount = timer;

        if(radialType == RadialType.Sink)
        {
            toolAudioSource.clip = ratchetClip;
            if(!toolAudioSource.isPlaying)
            {
                toolAudioSource.Play();
            }
        }
        if(radialType == RadialType.Window)
        {
            toolAudioSource.clip = hammerClip;
            if(!toolAudioSource.isPlaying)
            {
                toolAudioSource.Play();
            }            
        }
        if(radialType == RadialType.TV)
        {
            toolAudioSource.clip = ratchetClip;
            if(!toolAudioSource.isPlaying)
            {
                toolAudioSource.Play();
            }            
        }
        //spooky audio mid animation
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

        Debug.Log("timer:" + timer + "/" + maxTimer);
        if(timer >= maxTimer)
        {
            Debug.Log("Done");
            timer = 0.01f;
            radialImage.fillAmount = 0.01f;
            radialImage.enabled = false;
            radialDoneAudioSource.Play();

            if(radialType == RadialType.Sink)
            {
                //Disable sink VFX + SFX
                sinkDripAudioSource.Stop();
                waterDripEffect.SetActive(false);
                toolAudioSource.Stop();
                canUpdateSink = false;
                sinkObj.tag = "Untagged";

                gameController.currentCheckpoint = 4;

            }
            if(radialType == RadialType.Window)
            {
                toolAudioSource.Stop();
                canUpdateWindow = false;
                windowObj.tag = "Untagged";

                gameController.currentCheckpoint = 2;
            }
            if(radialType == RadialType.TV)
            {
                tvObj.tag = "Untagged";
                canUpdateTV = false;
                gameController.currentCheckpoint = 7;
            }
        }
        else
        {
            toolAudioSource.Pause();
        }
    }
}
