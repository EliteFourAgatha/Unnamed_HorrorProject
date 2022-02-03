using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour
{
    public AudioSource triggerAudio;

    private bool chamberLightTriggerActive = true;
    public GameObject affectedChamberLights;
    private bool fireScareTriggerActive = true;
    public GameObject fireScareObject;


    private bool canUseSnackMachine = false;
    private bool canUseLockedDoor = false;
    public enum TriggerType {Snacks, LockedDoor, ChamberLight, FireScare}
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
        if(canUseSnackMachine)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteAudioOnlyTrigger();
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
            else if(triggerType == TriggerType.LockedDoor)
            {
                canUseLockedDoor = true;
            }
            else if(triggerType == TriggerType.ChamberLight)
            {
                if(chamberLightTriggerActive)
                {
                    ExecuteChamberLightsTrigger();
                }
            }
            else if(triggerType == TriggerType.FireScare)
            {
                if(fireScareTriggerActive)
                {
                    ExecuteFireScareTrigger();
                }
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
            else if(triggerType == TriggerType.LockedDoor)
            {
                canUseLockedDoor = false;
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
    public void ExecuteChamberLightsTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        affectedChamberLights.SetActive(true);
        chamberLightTriggerActive = false;
    }
    public void ExecuteFireScareTrigger()
    {
        if(!triggerAudio.isPlaying)
        {
            triggerAudio.Play();
        }
        affectedChamberLights.SetActive(true);
        StartCoroutine(FlashFireScareObject());
        fireScareTriggerActive = false;
    }
    private IEnumerator FlashFireScareObject()
    {
        fireScareObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fireScareObject.SetActive(false);
    }
}