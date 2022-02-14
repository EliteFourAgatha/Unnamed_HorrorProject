using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    public AudioSource triggerAudio;

    public Light[] controlledLights;
    public GameObject lightSwitch;
    private bool lightsOff = true;
    public MainTriggers fuseBoxTriggerRef;

    void Start()
    {
        if(triggerAudio == null)
        {
            triggerAudio = gameObject.GetComponent<AudioSource>();
        }
    }
    public void UseLightSwitch()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        //Turn lights on
        if(lightsOff)
        {
            lightsOff = false;
            //Rotate lightswitch to "on"
            lightSwitch.transform.eulerAngles = new Vector3(-60, 0, 0);
            if(fuseBoxTriggerRef.breakerOn)
            {
                foreach(Light light in controlledLights)
                {
                    light.enabled = true;
                }
            }
        }
        //Turn lights off
        else
        {
            lightsOff = true;
            //Rotate lightswitch to "off"
            lightSwitch.transform.eulerAngles = new Vector3(-20, 0, 0);
            if(fuseBoxTriggerRef.breakerOn)
            {
                foreach(Light light in controlledLights)
                {
                    light.enabled = false;
                }
            }
        }
    }
}
