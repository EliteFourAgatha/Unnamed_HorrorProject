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
    private Rigidbody playerRB;
    private float moveForward;
    private float moveSide;
    public float fallingVelocity = 5f;
    private float currentSpeed;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    private bool canRun = true;
    public float maxStamina = 100f;
    private float currentStamina;
    private bool isGrounded;
    public LayerMask groundLayer;

    private void Awake()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        footstepAudioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        currentStamina = maxStamina;
        isGrounded = true;
    }
    private void Update()
    {
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
        
        //Debug.Log(currentStamina);
        
        //If player not moving
        if(playerRB.velocity.z == 0 && playerRB.velocity.x == 0)
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

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    public void MovePlayer()
    {
        playerRB.velocity = (moveForward * transform.forward) + (transform.right * moveSide);
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
        HandleStairs();
        //Vector3 move = transform.right * x + transform.forward * z;
        //controller.Move(move * speed * Time.deltaTime);

    }
    public void HandleStairs()
    {
        RaycastHit hit;
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
        if(!isGrounded)
        {
            playerRB.AddForce(-Vector3.up * fallingVelocity);
            Debug.Log("Dropping player via gravity");
        }
    }    
    public void PlayFootstepAudio()
    {
        if(currentSpeed == sprintSpeed)
        {
            footstepAudioSource.clip = sprintAudio;
            footstepAudioSource.Play();
        }
        else
        {
            footstepAudioSource.clip = walkAudio;
            footstepAudioSource.Play();
        }
    }
    public void PauseFootstepAudio()
    {
        footstepAudioSource.Pause();
    }

}


