using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour
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


    private bool canUseSnackMachine = false;
    private bool canUseLockedDoor = false;
    private bool playerSafe = false;
    public enum TriggerType {Snacks, LockedDoor, Safety, StorageLight, BackroomLight, BathroomLight,
                                 OfficeLight, LaundryLight}
    public TriggerType triggerType;
    void Start()
    {
        if(triggerAudio == null)
        {
            triggerAudio = gameObject.GetComponent<AudioSource>();
        }
        /*
        if(triggerType == TriggerType.Lightswitch || triggerType == TriggerType.FuseBox)
        {
            //Create array of materials from all used lights
            for(int i = 0; i < lightFoundations.Length; i++)
            {
                lightFoundationMaterials[i] = lightFoundations[i].material;
            }
            //Set reference to normal color of emission map, for turning lights on
            normalEmissionColor = lightFoundationMaterials[0].color;
        }
        if(triggerType == TriggerType.Closet)
        {
            brokenWindowMat = brokenWindowMesh.material;
            bathroomLightMat = bathroomLightMesh.material;
        }
        */
    }
    void Update()
    {
        if(playerSafe)
        {
            Debug.Log("Player safe!");
        }
        if(canUseSnackMachine)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteAudioOnlyTrigger();
            }
        }
        if(canUseLightSwitch)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteLightSwitchTrigger();
            }
        }
        if(canUseLockedDoor)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteAudioOnlyTrigger();
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(triggerType == TriggerType.Snacks)
            {
                canUseSnackMachine = true;
            }
            else if(triggerType == TriggerType.StorageLight)
            {
                canUseLightSwitch = true;
            }
            else if(triggerType == TriggerType.BackroomLight)
            {
                canUseLightSwitch = true;
            }
            else if(triggerType == TriggerType.OfficeLight)
            {
                canUseLightSwitch = true;
            }
            else if(triggerType == TriggerType.BathroomLight)
            {
                canUseLightSwitch = true;
            }
            else if(triggerType == TriggerType.LaundryLight)
            {
                canUseLightSwitch = true;
            }
            else if(triggerType == TriggerType.LockedDoor)
            {
                canUseLockedDoor = true;
            }
            else if(triggerType == TriggerType.Safety)
            {
                playerSafe = true;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(triggerType == TriggerType.Snacks)
            {
                canUseSnackMachine = false;
            }
            else if(triggerType == TriggerType.StorageLight)
            {
                canUseLightSwitch = false;
            }
            else if(triggerType == TriggerType.BackroomLight)
            {
                canUseLightSwitch = false;
            }
            else if(triggerType == TriggerType.OfficeLight)
            {
                canUseLightSwitch = false;
            }
            else if(triggerType == TriggerType.BathroomLight)
            {
                canUseLightSwitch = false;
            }
            else if(triggerType == TriggerType.LaundryLight)
            {
                canUseLightSwitch = false;
            }
            else if(triggerType == TriggerType.LockedDoor)
            {
                canUseLockedDoor = false;
            }
            if(triggerType == TriggerType.Safety)
            {
                playerSafe = false;
            }
        }
    }
    public void ExecuteAudioOnlyTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
    }
    public void ExecuteLightSwitchTrigger()
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
            if(triggerType == TriggerType.StorageLight)
            {
                _storageLightMat.SetColor("_EmissionColor", normalEmissionColor);
            }
            else if(triggerType == TriggerType.BackroomLight)
            {
                _backroomLightMat.SetColor("_EmissionColor", normalEmissionColor);
            }
            else if(triggerType == TriggerType.BathroomLight)
            {
                _bathroomLightMat.SetColor("_EmissionColor", normalEmissionColor);                
            }
            else if(triggerType == TriggerType.OfficeLight)
            {
                _officeLightMat.SetColor("_EmissionColor", normalEmissionColor);                
            }
            else if(triggerType == TriggerType.LaundryLight)
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
            if(triggerType == TriggerType.StorageLight)
            {
                _storageLightMat.SetColor("_EmissionColor", Color.black);
            }
            else if(triggerType == TriggerType.BackroomLight)
            {
                _backroomLightMat.SetColor("_EmissionColor", Color.black);
            }
            else if(triggerType == TriggerType.BathroomLight)
            {
                _bathroomLightMat.SetColor("_EmissionColor", Color.black);                
            }
            else if(triggerType == TriggerType.OfficeLight)
            {
                _officeLightMat.SetColor("_EmissionColor", Color.black);                
            }
            else if(triggerType == TriggerType.LaundryLight)
            {
                _laundryLightMat.SetColor("_EmissionColor", Color.black);                
            }
        }
    }
}