using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobEffect : MonoBehaviour
{
    public CharacterController playerController;
    public Animation anim;
    public Animator headBobAnimator;
    private bool isMoving;
    private bool left;
    private bool right;
    
    private void Start(){
        left = true;
        right = false;
    }

    // Update is called once per frame
    private void Update(){
        //CheckForMovementInput();
        //EnableHeadBobEffect();
    }
    public void EnableHeadBobEffect(){
        if(playerController.isGrounded == true){
            if(isMoving == true){
                if(left == true){
                    if(!anim.isPlaying){
                        anim.Play("WalkLeft");
                        Debug.Log("playing walk left bob");
                        left = false;
                        right = true;
                    }
                }
                if(right == true){
                    if(!anim.isPlaying){
                        anim.Play("WalkRight");
                        right = false;
                        left = true;
                    }
                }
            }
        }
    }
    public void CheckForMovementInput(){
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
