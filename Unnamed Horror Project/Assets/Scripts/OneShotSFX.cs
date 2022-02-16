using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSFX : MonoBehaviour
{
    public AudioSource audioSourceA;
    public AudioSource audioSourceB;
    public AudioSource audioSourceC;
    public AudioSource audioSourceD;
    public AudioSource audioSourceE;
    private bool playerMovedEnough = false;
    //private bool playerMoving;
    private float startTime;
    void Start()
    {
        
    }

    void Update()
    {
        DetectProlongedMovement();
        if(playerMovedEnough)
        {

        }
    }
    void DetectProlongedMovement()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            startTime = Time.time;
        }
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            //playerMoving = false;
        }
    }
}
