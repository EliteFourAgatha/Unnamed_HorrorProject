using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    public AudioSource triggerAudio;

    //Light switches
    public Light[] controlledLights;
    public GameObject lightSwitch;
    public MeshRenderer[] lightFoundations;
    public Material _backroomLightMat;
    public Material _storageLightMat;
    public Material _bathroomLightMat;
    public Material _officeLightMat;
    public Material _laundryLightMat;
    private Color normalEmissionColor;
    private bool lightsOff = true;
    private bool canUseLightSwitch = false;

    public enum SwitchType {StorageArea, Backroom, Bathroom, Office, Laundry, ChamberLight, FireScare}
    public SwitchType lightSwitchType;
    void Start()
    {
        if(triggerAudio == null)
        {
            triggerAudio = gameObject.GetComponent<AudioSource>();
        }
    }
    public void ExecuteLightSwitch()
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
            foreach(Light light in controlledLights)
            {
                light.enabled = true;
            }
            /*
            //Enable emission maps when lights turned on
            foreach(Material mat in lightFoundationMaterials)
            {
                mat.SetColor("_EmissionColor", normalEmissionColor);
            }
            */
            if(lightSwitchType == SwitchType.StorageArea)
            {
                _storageLightMat.SetColor("_EmissionColor", normalEmissionColor);
            }
            else if(lightSwitchType == SwitchType.Backroom)
            {
                _backroomLightMat.SetColor("_EmissionColor", normalEmissionColor);
            }
            else if(lightSwitchType == SwitchType.Bathroom)
            {
                _bathroomLightMat.SetColor("_EmissionColor", normalEmissionColor);                
            }
            else if(lightSwitchType == SwitchType.Office)
            {
                _officeLightMat.SetColor("_EmissionColor", normalEmissionColor);                
            }
            else if(lightSwitchType == SwitchType.Laundry)
            {
                _laundryLightMat.SetColor("_EmissionColor", normalEmissionColor);                
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
            /*
            //Disable emission maps when lights turned off
            foreach(Material mat in lightFoundationMaterials)
            {
                mat.SetColor("_EmissionColor", Color.black);
            }
            */
            if(lightSwitchType == SwitchType.StorageArea)
            {
                _storageLightMat.SetColor("_EmissionColor", Color.black);
            }
            else if(lightSwitchType == SwitchType.Backroom)
            {
                _backroomLightMat.SetColor("_EmissionColor", Color.black);
            }
            else if(lightSwitchType == SwitchType.Bathroom)
            {
                _bathroomLightMat.SetColor("_EmissionColor", Color.black);                
            }
            else if(lightSwitchType == SwitchType.Office)
            {
                _officeLightMat.SetColor("_EmissionColor", Color.black);                
            }
            else if(lightSwitchType == SwitchType.Laundry)
            {
                _laundryLightMat.SetColor("_EmissionColor", Color.black);                
            }
        }
    }
}
