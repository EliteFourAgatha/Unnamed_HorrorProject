using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerTest : MonoBehaviour
{
    private AudioSource footstepAudioSource;
    public AudioClip walkAudio;
    public AudioClip sprintAudio;
    public CharacterController controller;
    public Camera mainCamera;
    private float moveForward;
    private float moveHorizontal;
    private float gravityValue = 9.8f;
    private float verticalSpeed;
    private float currentSpeed;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    private bool canRun = true;
    public float maxStamina = 15f;
    private float currentStamina;
    private Rigidbody playerRB;

    private void Awake()
    {
        footstepAudioSource = gameObject.GetComponent<AudioSource>();
        playerRB = gameObject.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        currentStamina = maxStamina;
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(canRun)
            {
                currentSpeed = sprintSpeed;
            }
        }
        else
        {
            currentSpeed = walkSpeed;
        }
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");

        if(moveHorizontal != 0 || moveForward != 0)
        {
            MovePlayerTest(moveHorizontal, moveForward);
        }
        else
        {
            PauseFootstepAudio();
        }
        UpdateStamina();
        Debug.Log("Stamina: " + currentStamina);
    }    
    public void MovePlayerTest(float horizontal, float forward)
    {
        Vector3 moveVector = new Vector3(horizontal, 0.0f, forward);
        playerRB.velocity = moveVector * currentSpeed;
                
        //Vector3 move = transform.right * moveSide + transform.forward * moveForward;
        
        //Apply gravity to handle stairs and not float
        //verticalSpeed -= gravityValue * Time.deltaTime;

        //move.y = verticalSpeed;
        
        //controller.Move(move * Time.deltaTime);
        PlayFootstepAudio();
    }
    public void PlayFootstepAudio()
    {
        if(currentSpeed == sprintSpeed)
        {
            footstepAudioSource.clip = sprintAudio;
            if(!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
        else
        {
            footstepAudioSource.clip = walkAudio;
            if(!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
    }

    public void PauseFootstepAudio()
    {
        footstepAudioSource.Pause();
    }


    public void UpdateStamina()
    {
        //No stamina
        if(currentStamina <= 0)
        {
            //Player winded, play SFX
            canRun = false;
        }
        //Max stamina
        else if(currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
            canRun = true;
        }
        //Repair stamina
        else if(currentStamina <= maxStamina)
        {
            //If player moving
            if(moveHorizontal != 0 || moveForward != 0)
            {
                //Sprinting, lose stamina
                if(currentSpeed == sprintSpeed)
                {
                    currentStamina -= Time.deltaTime;
                }
                //Walking, repair stamina
                else
                {
                    currentStamina += Time.deltaTime;
                }
            }
        }
    }
}


