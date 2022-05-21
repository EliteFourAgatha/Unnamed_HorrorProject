using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private AudioSource footstepAudioSource;
    [SerializeField] private AudioSource windedAudioSource;
    public AudioClip concreteWalkSFX;
    public AudioClip concreteSprintSFX;
    public CharacterController controller;
    public Camera mainCamera;
    public bool canMove = true;
    private float moveForward;
    private float moveSide;
    private float gravityValue = 9.8f;
    private float verticalSpeed;
    private float currentSpeed;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    private bool canRun = true;
    public float maxStamina = 5f;
    private float currentStamina;
    void Awake()
    {
        footstepAudioSource = gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        currentStamina = maxStamina;
    }
    void Update()
    {
        if(canMove)
        {
            if(canRun)
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    currentSpeed = sprintSpeed;
                }
                else
                {
                    currentSpeed = walkSpeed;
                }
            }
            else
            {
                currentSpeed = walkSpeed;
            }

            moveSide = Input.GetAxis("Horizontal") * currentSpeed;
            moveForward = Input.GetAxis("Vertical") * currentSpeed;

            if(moveSide != 0 || moveForward != 0)
            {
                MovePlayer();
            }
            else
            {
                PauseFootstepAudio();
            }
            UpdateStamina();
            Debug.Log("Stamina: " + currentStamina);
        }

    }    
    private void MovePlayer()
    {
        //playerRB.velocity = (moveForward * transform.forward) + (transform.right * moveSide);
        Vector3 move = transform.right * moveSide + transform.forward * moveForward;
        //Apply gravity to handle stairs and not float
        verticalSpeed -= gravityValue * Time.deltaTime;
        move.y = verticalSpeed;
        
        controller.Move(move * Time.deltaTime);
        PlayFootstepAudio();
    }
    private void PlayFootstepAudio()
    {
        if(currentSpeed == sprintSpeed)
        {
            footstepAudioSource.clip = concreteSprintSFX;
            if(!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
        else
        {
            footstepAudioSource.clip = concreteWalkSFX;
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
    private void UpdateStamina()
    {
        if(currentStamina <= 0)
        {
            windedAudioSource.Play();
            canRun = false;
        }
        else if(currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
            canRun = true;
        }
        //If player moving
        if(moveSide != 0 || moveForward != 0)
        {
            //Sprinting, lose stamina
            if(currentSpeed == sprintSpeed)
            {
                currentStamina -= 1 * Time.deltaTime;
            }
            //Walking, repair stamina
            else
            {
                currentStamina += 1 * Time.deltaTime;
            }
        }
    }
}


