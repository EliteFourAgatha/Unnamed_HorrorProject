using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//
//Consider moving this to trigger class as well, now that it's just 1 trigger
//
//
public class DoorController : MonoBehaviour
{
    //Serialize field allows you to edit in inspector while keeping private
    [SerializeField] private Animator myDoor;
    //private AudioSource doorAudioSource;
    public bool doorClosed = true;
    public AudioClip doorOpenOne;
    public AudioClip doorCloseOne;
    //Normal door is most doors
    // Apartment door is main entrance, opens to different angle
    public enum DoorType {normalDoor, apartmentDoor};
    public DoorType doorType;
    public void OpenDoor()
    {
        if(doorType == DoorType.normalDoor)
        {
            myDoor.Play("DoorOpen", 0, 0.0f);
        }
        else if(doorType == DoorType.apartmentDoor)
        {
            myDoor.Play("FrontDoorOpen", 0, 0.0f);
        }
        PlayRandomOpenSFX();
        //Wait for cooldown? Avoid spamming open/Wait for sfx/Reset doorClosed bool
        doorClosed = false;
    }

    public void CloseDoor()
    {
        if(doorType == DoorType.normalDoor)
        {
            myDoor.Play("DoorClose", 0, 0.0f);
        }
        else if(doorType == DoorType.apartmentDoor)
        {
            myDoor.Play("FrontDoorClose", 0, 0.0f);
        }
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
