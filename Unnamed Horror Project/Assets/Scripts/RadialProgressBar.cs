using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private float maxTimer = 5.0f;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource radialDoneAudioSource;
    [SerializeField] private AudioSource toolAudioSource;
    public AudioClip ratchetClip;
    public AudioClip hammerClip;


    [Header("Sink")]
    public Image sinkRadialImage;
    [SerializeField] private AudioSource sinkScareAudioSource;
    [SerializeField] private AudioSource sinkDripAudioSource;
    [SerializeField] private GameObject waterDripEffect;
    [SerializeField] private GameObject sinkObj;
    public bool canUpdateSink = false;
    private float sinkTimer = 0.01f;
    private float sinkDelayValue = 4f;

    [Header("Window + TV")]
    public Image windowRadialImage;
    public Image tvRadialImage;
    [SerializeField] private GameObject tvObj;
    [SerializeField] private GameObject windowObj;
    public bool canUpdateWindow = false;
    public bool canUpdateTV = false;
    private float windowTimer = 0.01f;
    private float windowDelayValue = 4f;
    private float tvTimer = 0.01f;
    private float tvDelayValue = 4f;

    private float distance;

    void Update()
    {
        //
        // Instead of enable/disable image consider doing setactive the object itself
        //
        //


        //Debug.Log("sink status: " + canUpdateSink);
        if(canUpdateSink)
        {
            sinkRadialImage.enabled = true;
            if(Input.GetKey(KeyCode.E))
            {
                UpdateSinkRadialFill();
            }
        }
        else
        {
            sinkRadialImage.enabled = false;
        }
        if(canUpdateWindow)
        {
            windowRadialImage.enabled = true;
            if(Input.GetKey(KeyCode.E))
            {
                UpdateWindowRadialFill();
            }
        }
        else
        {
            windowRadialImage.enabled = false;
        }
        if(canUpdateTV)
        {
            tvRadialImage.enabled = true;
            if(Input.GetKey(KeyCode.E))
            {
                UpdateTVRadialFill();
            }
        }
        else
        {
            tvRadialImage.enabled = false;
        }
    }

    public void UpdateSinkRadialFill()
    {
        sinkTimer += Time.deltaTime / sinkDelayValue;
        sinkRadialImage.fillAmount = sinkTimer;
        toolAudioSource.clip = ratchetClip;
        //Debug.Log("sinkTimer:" + sinkTimer);
        //Debug.Log("radial%:" + sinkRadialImage.fillAmount);

        if(!toolAudioSource.isPlaying)
        {
            toolAudioSource.Play();
        }
        //spooky audio mid animation
        if(sinkTimer == maxTimer / 3)
        {
            sinkScareAudioSource.Play();
            Debug.Log("spooked");
        }
        if(sinkTimer >= 1)
        {
            Debug.Log("Done");
            sinkTimer = 0.01f;
            sinkRadialImage.fillAmount = 0.01f;
            sinkRadialImage.enabled = false;
            radialDoneAudioSource.Play();

            //Disable sink VFX + SFX
            sinkDripAudioSource.Stop();
            waterDripEffect.SetActive(false);
            toolAudioSource.Stop();
            canUpdateSink = false;
            sinkObj.tag = "Untagged";

            gameController.currentCheckpoint = 4;
        }
        else
        {
            toolAudioSource.Pause();
        }
    }

    public void UpdateWindowRadialFill()
    {
        windowTimer += Time.deltaTime;
        windowRadialImage.fillAmount = windowTimer;

        toolAudioSource.clip = hammerClip;
        if(!toolAudioSource.isPlaying)
        {
            toolAudioSource.Play();
        }            
        //spooky audio mid animation
        if(windowTimer == maxTimer / 3)
        {
            Debug.Log("spooked");
        }

        if(windowTimer >= maxTimer)
        {
            Debug.Log("Done");
            windowTimer = 0.01f;
            windowRadialImage.fillAmount = 0.01f;
            windowRadialImage.enabled = false;
            radialDoneAudioSource.Play();

            toolAudioSource.Stop();
            canUpdateWindow = false;
            windowObj.tag = "Untagged";

            gameController.currentCheckpoint = 2;
        }
        else
        {
            toolAudioSource.Pause();
        }
    }

    public void UpdateTVRadialFill()
    {
        tvTimer += Time.deltaTime;
        tvRadialImage.fillAmount = tvTimer;

        toolAudioSource.clip = ratchetClip;
        if(!toolAudioSource.isPlaying)
        {
            toolAudioSource.Play();
        }
        //spooky audio mid animation
        if(tvTimer == maxTimer / 3)
        {
            Debug.Log("spooked");
        }

        if(tvTimer >= maxTimer)
        {
            Debug.Log("TV Done");
            tvTimer = 0.01f;
            tvRadialImage.fillAmount = 0.01f;
            tvRadialImage.enabled = false;
            radialDoneAudioSource.Play();

            tvObj.tag = "Untagged";
            canUpdateTV = false;
            gameController.currentCheckpoint = 7;

            //disable basement lights (lights go out moment)
            //enable blur scare trigger in hallway
            //enable final closet trigger

            //ultimatum moment
            //-If player goes right, can leave and sensible ending possible
            //-If player goes left, trigger final closet and top of stairs door
        }
        else
        {
            toolAudioSource.Pause();
        }
    }
}
