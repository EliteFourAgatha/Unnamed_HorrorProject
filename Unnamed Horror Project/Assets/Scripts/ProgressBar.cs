using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource progressDoneAudioSource;
    [SerializeField] private AudioSource toolAudioSource;
    [SerializeField] private GameObject finalClosetTrigger;
    public enum ProgressType{Sink, Window, VendMachine}
    public ProgressType progressType;
    public AudioClip ratchetClip;
    public AudioClip hammerClip;
    private float distance;


    [Header("Sink")]
    public Image sinkProgressBar;
    [SerializeField] private GameObject sinkObj;
    [SerializeField] private GameObject sinkLeakObject;
    public bool canUpdateSink = false;
    private float sinkTimer = 0.01f;
    [SerializeField] private float sinkDelayValue = 4f;
    private string sinkMessage = "'Fix the broken vending machine in the lobby'";

    [Header("Laundry Window")]
    public Image windowProgressBar;
    [SerializeField] private GameObject windowObj;
    public bool canUpdateWindow = false;
    private float windowTimer = 0.01f;
    [SerializeField] private float windowDelayValue = 4f;
    private string windowMessage = "'Turn off lights / lock up before you leave'";


    [Header("VendMachine")]
    public Image vendMachineProgressBar;
    [SerializeField] private GameObject VendMachineObj;
    public bool canUpdateVendMachine = false;
    private float VendMachineTimer = 0.01f;
    [SerializeField] private float vendMachineDelayValue = 5f;
    private string vendMachineMessage = "'Take the box in storage and put it in my office'";

    void Update()
    {
        //
        // Instead of enable/disable image consider doing setactive the object itself
        //
        //


        //Debug.Log("sink status: " + canUpdateSink);
        /*
        if(progressType == ProgressType.Sink)
        {
            if(gameController.currentCheckpoint == 2)
            {
                if(canUpdateSink)
                {
                    sinkProgressBar.enabled = true;
                    if(Input.GetKey(KeyCode.E))
                    {
                        UpdateSinkFill();
                    }
                }
                else
                {
                    sinkProgressBar.enabled = false;
                }
            }
        }

        if(progressType == ProgressType.Window)
        {
            if(gameController.currentCheckpoint == 7)
            {
                if(canUpdateWindow)
                {
                    windowProgressBar.enabled = true;
                    if(Input.GetKey(KeyCode.E))
                    {
                        UpdateWindowFill();
                    }
                }
                else
                {
                    windowProgressBar.enabled = false;
                } 
            }
        }

        if(progressType == ProgressType.VendMachine)
        {
            if(gameController.currentCheckpoint == 3)
            {
                if(canUpdateVendMachine)
                {
                    vendMachineProgressBar.enabled = true;
                    if(Input.GetKey(KeyCode.E))
                    {
                        UpdateVendMachineFill();
                    }
                }
                else
                {
                    vendMachineProgressBar.enabled = false;
                }
            }
        }
        */
    }

    public void UpdateSinkFill()
    {
        sinkTimer += Time.deltaTime / sinkDelayValue;
        sinkProgressBar.fillAmount = sinkTimer;
        toolAudioSource.clip = ratchetClip;

        if(sinkTimer >= 1)
        {
            sinkTimer = 0f;
            sinkProgressBar.fillAmount = 0f;
            sinkProgressBar.enabled = false;
            progressDoneAudioSource.Play();

            //Disable sink VFX + SFX
            sinkLeakObject.SetActive(false);
            toolAudioSource.Stop();
            sinkObj.tag = "Untagged";
            
            StartCoroutine(gameController.ShowPopupMessage(sinkMessage, 2f));

            gameController.currentCheckpoint = 3;
        }
    }

    public void UpdateWindowFill()
    {
        windowTimer += Time.deltaTime / windowDelayValue;
        windowProgressBar.fillAmount = windowTimer;
        toolAudioSource.clip = hammerClip;

        if(windowTimer >= 1)
        {
            windowTimer = 0f;
            windowProgressBar.fillAmount = 0f;
            windowProgressBar.enabled = false;
            progressDoneAudioSource.Play();

            toolAudioSource.Stop();
            windowObj.tag = "Untagged";

            StartCoroutine(gameController.ShowPopupMessage(windowMessage, 2f));

            gameController.currentCheckpoint = 9;
            finalClosetTrigger.SetActive(true);
        }
    }

    public void UpdateVendMachineFill()
    {
        toolAudioSource.clip = ratchetClip;
        VendMachineTimer += Time.deltaTime / vendMachineDelayValue;
        vendMachineProgressBar.fillAmount = VendMachineTimer;

        if(VendMachineTimer >= 1)
        {
            VendMachineTimer = 0f;
            vendMachineProgressBar.fillAmount = 0f;
            vendMachineProgressBar.enabled = false;
            progressDoneAudioSource.Play();

            toolAudioSource.Stop();
            VendMachineObj.tag = "Untagged";

            StartCoroutine(gameController.ShowPopupMessage(vendMachineMessage, 2f));

            gameController.currentCheckpoint = 4;
        }
    }
}
