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
    public AudioClip doorSFXOne;
    private void Awake(){
        //doorAudioSource = gameObject.GetComponent<AudioSource>();
    }
    public void Update(){
        if(canChangeDoorState){
            if(Input.GetKeyDown(KeyCode.E)){
                ChangeDoorState();
            }
        }
    }

    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            canChangeDoorState = true;
        }
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
