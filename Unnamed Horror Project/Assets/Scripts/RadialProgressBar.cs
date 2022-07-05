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
    public enum RadialType{Sink, Window, VendMachine}
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
    private string sinkMessage = "'Fix the broken vending machine in the lobby'";

    [Header("Laundry Window")]
    public Image windowRadialImage;
    [SerializeField] private GameObject windowObj;
    public bool canUpdateWindow = false;
    private float windowTimer = 0.01f;
    private float windowDelayValue = 4f;


    [Header("VendMachine")]
    public Image VendMachineRadialImage;
    [SerializeField] private GameObject VendMachineObj;
    public bool canUpdateVendMachine = false;
    private float VendMachineTimer = 0.01f;
    private float VendMachineDelayValue = 4f;


    void Update()
    {
        //
        // Instead of enable/disable image consider doing setactive the object itself
        //
        //


        //Debug.Log("sink status: " + canUpdateSink);
        if(radialType == RadialType.Sink)
        {
            if(gameController.currentCheckpoint == 2)
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
        }

        if(radialType == RadialType.Window)
        {
            if(gameController.currentCheckpoint == 7)
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
        }

        if(radialType == RadialType.VendMachine)
        {
            if(gameController.currentCheckpoint == 3)
            {
                if(canUpdateVendMachine)
                {
                    VendMachineRadialImage.enabled = true;
                    if(Input.GetKey(KeyCode.E))
                    {
                        UpdateVendMachineRadialFill();
                    }
                }
                else
                {
                    VendMachineRadialImage.enabled = false;
                }
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
            Debug.Log("sink Done");
            sinkTimer = 0.01f;
            sinkRadialImage.fillAmount = 0.01f;
            sinkRadialImage.enabled = false;
            radialDoneAudioSource.Play();

            //Disable sink VFX + SFX
            sinkLeakObject.SetActive(false);
            toolAudioSource.Stop();
            canUpdateSink = false;
            sinkObj.tag = "Untagged";
            
            StartCoroutine(gameController.ShowPopupMessage(sinkMessage, 2f));

            gameController.currentCheckpoint = 3;
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

    public void UpdateVendMachineRadialFill()
    {
        VendMachineTimer += Time.deltaTime;
        VendMachineRadialImage.fillAmount = VendMachineTimer;

        toolAudioSource.clip = ratchetClip;
        if(!toolAudioSource.isPlaying)
        {
            toolAudioSource.Play();
        }

        if(VendMachineTimer >= maxTimer)
        {
            Debug.Log("VendMachine Done");
            VendMachineTimer = 0.01f;
            VendMachineRadialImage.fillAmount = 0.01f;
            VendMachineRadialImage.enabled = false;
            radialDoneAudioSource.Play();

            VendMachineObj.tag = "Untagged";
            canUpdateVendMachine = false;
            gameController.currentCheckpoint = 4;
        }
        else
        {
            toolAudioSource.Pause();
        }
    }
}
