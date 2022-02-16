using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Triggers : MonoBehaviour
{
    public Animation playerAnim;
    public AudioSource triggerAudio;

    public Texture2D hideOnCouchTexture;
    public Texture2D normalCursorTexture;

    private bool chamberLightTriggerActive = true;
    public GameObject affectedChamberLights;
    private bool fireScareTriggerActive = true;
    public GameObject fireScareObject;

    public HideAnimations hideAnimations;
    bool isHiding;


    private bool canInteractWithObject = false;
    private bool canUseHideAnimation = false;
    public enum TriggerType {Snacks, LockedDoor, ChamberLight, FireScare, CouchHide}
    public TriggerType triggerType;
    void Start()
    {
        isHiding = false;
        if(triggerAudio == null)
        {
            triggerAudio = gameObject.GetComponent<AudioSource>();
        }
        if(playerAnim == null)
        {
            playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animation>();
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
        if(canInteractWithObject)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ExecuteAudioOnlyTrigger();
            }
        }
        if(canUseHideAnimation)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(isHiding)
                {
                    UnhideFromCouch();
                }
                else
                {
                    HideBehindCouch();
                }
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(triggerType == TriggerType.Snacks)
            {
                canInteractWithObject = true;
            }
            else if(triggerType == TriggerType.LockedDoor)
            {
                canInteractWithObject = true;
            }
            else if(triggerType == TriggerType.CouchHide)
            {
                canUseHideAnimation = true;
                Cursor.SetCursor(hideOnCouchTexture, Vector2.zero, CursorMode.Auto);
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
                canInteractWithObject = false;
            }
            else if(triggerType == TriggerType.LockedDoor)
            {
                canInteractWithObject = false;
            }
            else if(triggerType == TriggerType.CouchHide)
            {
                canUseHideAnimation = false;
                Cursor.SetCursor(normalCursorTexture, Vector2.zero, CursorMode.Auto);
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
    public void HideBehindCouch()
    {        
        hideAnimations.HideBehindCouch();
        isHiding = true;
    }
    public void UnhideFromCouch()
    {
        hideAnimations.UnhideFromCouch();
        isHiding = false;
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