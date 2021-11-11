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
    public AudioClip doorSFXOne;
    private void Awake(){
        //doorAudioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start(){
        doorClosed = true;
    }
    public void Update(){
        if(canChangeDoorState){ // If in range of door
            if(Input.GetKeyDown(KeyCode.E)){ // If player presses E
                if(doorClosed) // If door currently closed
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

    private void OpenDoor(){
        myDoor.Play("DoorOpen", 0, 0.0f);
        Debug.Log("door open");
        canChangeDoorState = false;
        PlayRandomDoorSFX();

        //Wait for cooldown? Avoid spamming open/Wait for sfx/Reset doorClosed bool
        doorClosed = false;
    }

    private void CloseDoor(){
        myDoor.Play("DoorClose", 0, 0.0f);
        Debug.Log("door close");
        canChangeDoorState = false;
        PlayRandomDoorSFX();

        //Wait for cooldown? Avoid spamming open/Wait for sfx/Reset doorClosed bool
        doorClosed = true;
    }

    private void ChangeDoorState(){
        if(openTrigger){
            //Animation name, stateNameHash, normalized time
            myDoor.Play("DoorOpen", 0, 0.0f);
            Debug.Log("door open");
            PlayRandomDoorSFX();
            canChangeDoorState = false;
            gameObject.SetActive(false);
        }
        else if(closeTrigger){
            myDoor.Play("DoorClose", 0, 0.0f);
            Debug.Log("door close");
            PlayRandomDoorSFX();
            canChangeDoorState = false;
            gameObject.SetActive(false);
        }
    }
    //
    //Need to randomize this, currently only playing same sfx (polish)
    //
    public void PlayRandomDoorSFX(){
        //if(!doorAudioSource.isPlaying)
        AudioSource.PlayClipAtPoint(doorSFXOne, transform.position, 0.5f);
    }
}
