using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    private AudioSource audioSource;
    private bool sinkWaterOn;
    private bool canChangeSinkState;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        sinkWaterOn = false;
        canChangeSinkState = false;
    }

    private void Update()
    {
        if(canChangeSinkState)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(!sinkWaterOn)
                {
                    StartSinkWater();
                }
                else
                {
                    StopSinkWater();
                }
            }
        }
    }

    public void StartSinkWater()
    {
        audioSource.Play();
        sinkWaterOn = true;
        //start VFX
    }
    public void StopSinkWater()
    {
        audioSource.Stop();
        sinkWaterOn = false;
        //end VFX
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canChangeSinkState = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canChangeSinkState = false;
        }
    }
}
