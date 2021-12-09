using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private AudioSource footstepAudioSource;
    public AudioClip walkAudio;
    public AudioClip sprintAudio;
    public CharacterController controller;
    public Camera mainCamera;
    //private Rigidbody playerRB;
    private float moveForward;
    private float moveSide;
    public float fallingVelocity = 100f;
    private float gravityValue = 9.8f;
    private float verticalSpeed;
    private float currentSpeed;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    private bool canRun = true;
    public float maxStamina = 100f;
    private float currentStamina;
    private bool isGrounded;
    public LayerMask groundLayer;
    private Vector3 lastPosition;

    private void Awake()
    {
        //playerRB = gameObject.GetComponent<Rigidbody>();
        footstepAudioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        currentStamina = maxStamina;
        isGrounded = true;
    }
    private void Update()
    {
        lastPosition = new Vector3(0f, 0f, 0f);
        gameObject.transform.position = lastPosition;
        //Player winded, play SFX
        if(currentStamina <= 0)
        {
            canRun = false;
        }

        if(Input.GetKey("space"))
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
        moveSide = Input.GetAxis("Horizontal") * currentSpeed;
        moveForward = Input.GetAxis("Vertical") * currentSpeed;

        if(Time.timeScale == 1)
        {
            MovePlayer();
        }


        
        //If player not moving
        
        if(lastPosition != gameObject.transform.position)
        {
            Debug.Log("player not moving");
            PauseFootstepAudio();
            if(currentStamina <= maxStamina)
            {
                currentStamina += 1 * Time.deltaTime;
            }
        }
        if(isGrounded)
        {
            Debug.Log("Player grounded");
        }
    }    
    public void MovePlayer()
    {
        //playerRB.velocity = (moveForward * transform.forward) + (transform.right * moveSide);
        Vector3 move = transform.right * moveSide + transform.forward * moveForward;
        verticalSpeed -= gravityValue * Time.deltaTime;
        move.y = verticalSpeed;
        
        controller.Move(move * Time.deltaTime);
        PlayFootstepAudio();
        if(currentSpeed == sprintSpeed)
        {
            currentStamina -= 1 * Time.deltaTime;
        }
        else
        {
            if(currentStamina >= maxStamina)
            {
                currentStamina += 1 * Time.deltaTime;
            }
        }

        //HandleStairs();
    }
    public void HandleStairs()
    {
        //RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        Vector3 targetPosition;
        targetPosition = transform.position;
        /*

        if(Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            Vector3 raycastHitpoint = hit.point;
            targetPosition.y = raycastHitpoint.y;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if(isGrounded)
        {
            if(playerRB.velocity.z != 0 || playerRB.velocity.x != 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
        }
        else
        {
            transform.position = targetPosition;
        }
        */
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

}


