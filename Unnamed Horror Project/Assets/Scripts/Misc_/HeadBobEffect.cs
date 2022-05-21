using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobEffect : MonoBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private Animation anim;
    private bool isMoving;
    private bool isRunning;
    private bool left;
    private bool right;
    
    void Start()
    {
        left = true;
        right = false;
    }
    void Update()
    {
        //Check if player is moving
        CheckForMovementInput();
        //If so, enable head bob effect
        EnableHeadBobEffect();
    }
    void EnableHeadBobEffect()
    {
        if(playerController.isGrounded)
        {
            if(isMoving)
            {
                if(isRunning)
                {
                    AlternateLeftRightRun();
                }
                else
                {
                    AlternateLeftRightWalk();
                }

            }
        }
    }
    void AlternateLeftRightRun()
    {
        if(left == true)
        {
            if(!anim.isPlaying)
            {
                anim.Play("RunLeft");
                left = false;
                right = true;
            }
        }
        if(right == true)
        {
            if(!anim.isPlaying)
            {
                anim.Play("RunRight");
                right = false;
                left = true;
            }
        }
    }
    void AlternateLeftRightWalk()
    {
        if(left == true)
        {
            if(!anim.isPlaying)
            {
                anim.Play("WalkLeft");
                left = false;
                right = true;
            }
        }
        if(right == true)
        {
            if(!anim.isPlaying)
            {
                anim.Play("WalkRight");
                right = false;
                left = true;
            }
        }
    }
    void CheckForMovementInput()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        if(inputX != 0 || inputY != 0){
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
