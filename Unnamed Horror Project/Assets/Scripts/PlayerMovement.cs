using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public Slider staminaBar;
    private bool isWalking = false;
    private bool isRunning = false;
    private bool canRun = true;
    private int staminaValue;
    private Vector3 velocity;
    private Footsteps footsteps;
    private void Awake(){
        footsteps = gameObject.GetComponent<Footsteps>();
    }
    private void Start(){
        staminaValue = 100;
    }
    private void Update(){
        if(staminaValue <= 0){
            canRun = false;
        }
        if(Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal") == -1){
            //if space bar is held down, run.
            if(Input.GetKey("space")){
                speed = 12f;
                staminaValue --;
                isRunning = true;
                MovePlayer();
                print(staminaValue);
                print("isrunning");
            }
            else{
                speed = 6f;
                MovePlayer();
                isWalking = true;
                footsteps.PlayFootstepAudio();
                print("iswalking");
            }
        }
        else if(Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1){
            //if space bar is held down, run.
            if(Input.GetKey("space")){
                speed = 12f;
                staminaValue --;
                isRunning = true;
                MovePlayer();
                isWalking = false;
                print(staminaValue);
            }
            else{
                speed = 6f;
                isWalking = true;
                MovePlayer();
                isRunning = false;
                footsteps.PlayFootstepAudio();
            }
        }
        else{
            isWalking = false;
            isRunning = false;
            footsteps.PauseFootstepAudio();
            print("pausedfootsteps");
        }

    }

    public void MovePlayer(){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
}
