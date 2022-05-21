using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    [SerializeField] private AudioSource triggerAudio;
    [SerializeField] private GameController gameController;

    [SerializeField] private Light[] controlledLights;
    [SerializeField] private GameObject lightSwitch;
    private bool lightsOff = true;
    [SerializeField] private enum LightType {breaker, nonBreaker};
    [SerializeField] private LightType lightType;

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
        if(lightType == LightType.breaker)
        {
            //Make sure breaker is on so lights function properly
            if(gameController.breakerOn)
            {
                //Turn lights on
                if(lightsOff)
                {
                    lightsOff = false;
                    //Rotate lightswitch to "on"
                    lightSwitch.transform.eulerAngles = new Vector3(-60, 0, 0);
                    foreach(Light light in controlledLights)
                    {
                        light.enabled = true;
                    }
                }
                //Turn lights off
                else
                {
                    lightsOff = true;
                    //Rotate lightswitch to "off"
                    lightSwitch.transform.eulerAngles = new Vector3(-20, 0, 0);
                    foreach(Light light in controlledLights)
                    {
                        light.enabled = false;
                    }
                }
            }
        }
        else if(lightType == LightType.nonBreaker)
        {
            //Turn lights on
            if(lightsOff)
            {
                lightsOff = false;
                //Rotate lightswitch to "on"
                lightSwitch.transform.eulerAngles = new Vector3(-60, 0, 0);
                foreach(Light light in controlledLights)
                {
                    light.enabled = true;
                }
            }
            //Turn lights off
            else
            {
                lightsOff = true;
                //Rotate lightswitch to "off"
                lightSwitch.transform.eulerAngles = new Vector3(-20, 0, 0);
                foreach(Light light in controlledLights)
                {
                    light.enabled = false;
                }
            }            
        }
    }
}
