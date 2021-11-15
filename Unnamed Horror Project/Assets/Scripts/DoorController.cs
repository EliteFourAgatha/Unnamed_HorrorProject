using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //Serialize field allows you to edit in inspector while keeping private
    [SerializeField] private Animator myDoor;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    //private AudioSource doorAudioSource;
    private bool canChangeDoorState;
    private bool doorClosed;
    public AudioClip doorOpenOne;
    public AudioClip doorCloseOne;
    private void Awake()
    {
        //doorAudioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        doorClosed = true;
    }
    public void Update()
    {
        //If in range of door
        if(canChangeDoorState)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(doorClosed)
                {
                    OpenDoor();
                }
                else
                {
                    CloseDoor();
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            canChangeDoorState = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canChangeDoorState = false;
        }
    }

    private void OpenDoor()
    {
        myDoor.Play("DoorOpen", 0, 0.0f);
        Debug.Log("door open");
        canChangeDoorState = false;
        PlayRandomOpenSFX();

        //Wait for cooldown? Avoid spamming open/Wait for sfx/Reset doorClosed bool
        doorClosed = false;
    }

    private void CloseDoor()
    {
        myDoor.Play("DoorClose", 0, 0.0f);
        Debug.Log("door close");
        canChangeDoorState = false;
        PlayRandomCloseSFX();

        //Wait for cooldown? Avoid spamming open/Wait for sfx/Reset doorClosed bool
        doorClosed = true;
    }
    //
    //Need to randomize this, currently only playing same sfx (polish)
    //
    public void PlayRandomOpenSFX()
    {
        //if(!doorAudioSource.isPlaying)
        AudioSource.PlayClipAtPoint(doorOpenOne, transform.position, 0.5f);
    }
    public void PlayRandomCloseSFX()
    {
        AudioSource.PlayClipAtPoint(doorCloseOne, transform.position, 0.5f);
    }
}
