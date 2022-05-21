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
    [SerializeField] private Animator myDoor;
    [SerializeField] private bool doorClosed = true;
    [SerializeField] private AudioClip doorOpenOne;
    [SerializeField] private AudioClip doorCloseOne;
    //Normal door is most doors
    // Apartment door is main entrance, opens to different angle
    [SerializeField] private enum DoorType {normalDoor, apartmentDoor};
    [SerializeField] private DoorType doorType;
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
