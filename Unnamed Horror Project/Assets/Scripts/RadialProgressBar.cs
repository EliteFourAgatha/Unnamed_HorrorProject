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
    public enum RadialType{Sink, Window, TV}
    public RadialType radialType;
    public AudioClip ratchetClip;
    public AudioClip hammerClip;
    private float distance;


    [Header("Sink")]
    public Image sinkRadialImage;
    [SerializeField] private GameObject sinkObj;
    [SerializeField] private GameObject sinkLeakObject;
    public bool canUpdateSink = false;
    private float sinkTimer = 0.01f;
    private float sinkDelayValue = 4f;

    [Header("Laundry Window")]
    public Image windowRadialImage;
    [SerializeField] private GameObject windowObj;
    public bool canUpdateWindow = false;
    private float windowTimer = 0.01f;
    private float windowDelayValue = 4f;


    [Header("TV")]
    public Image tvRadialImage;
    [SerializeField] private GameObject tvObj;
    public bool canUpdateTV = false;
    private float tvTimer = 0.01f;
    private float tvDelayValue = 4f;
    [SerializeField] private GameObject blownFuseTrigger;
    [SerializeField] private GameObject shadowBlurTrigger;

    void Update()
    {
        //
        // Instead of enable/disable image consider doing setactive the object itself
        //
        //


        //Debug.Log("sink status: " + canUpdateSink);
        if(radialType == RadialType.Sink)
        {
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
        }

        if(radialType == RadialType.Window)
        {
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
        }

        if(radialType == RadialType.TV)
        {
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
        if(sinkTimer >= 1)
        {
            Debug.Log("Done");
            sinkTimer = 0.01f;
            sinkRadialImage.fillAmount = 0.01f;
            sinkRadialImage.enabled = false;
            radialDoneAudioSource.Play();

            //Disable sink VFX + SFX
            sinkLeakObject.SetActive(false);
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
