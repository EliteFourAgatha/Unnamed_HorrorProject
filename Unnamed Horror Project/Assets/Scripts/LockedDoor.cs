using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for locked door SFX and cursor change
// Placed on locked door child object
public class LockedDoor : MonoBehaviour
{
    private AudioSource audioSource;
    private bool canCheckLockedDoor;
    public Cursor handCursor;
    
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        canCheckLockedDoor = false;
    }
    private void Update()
    {
        if(canCheckLockedDoor)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Check player orientation? Looking at door?
                // Maybe OnMouseEnter on door itself. If so, change cursor.
                // (Then use OnMouseExit to change cursor back when not hovering)
                
                //Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);

                //  Then just check if E is pressed here, SFX + "Locked" popup UI etc.
                if(!audioSource.isPlaying)
                {
                    audioSource.Play();
                    Debug.Log("Door locked!");
                }
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canCheckLockedDoor = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canCheckLockedDoor = false;
        }
    }
}
