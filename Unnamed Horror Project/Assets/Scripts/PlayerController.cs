using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip concreteWalkSFX;
    [SerializeField] private AudioClip concreteSprintSFX;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera mainCamera;


    [Header("Movement Parameters")]
    [SerializeField, Range(1, 10)] private float walkSpeed = 5f;
    [SerializeField, Range(1, 20)] private float sprintSpeed = 10f;
    [SerializeField, Range(1, 5)] private float crouchSpeed = 2.5f;
    private float currentSpeed;
    private float gravityValue = 9.8f;
    private float verticalSpeed;
    public bool canMove = true;
    private float moveForward;
    private float moveSide;
    private bool canRun = true;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2.1f;
    [SerializeField] private float timeToCrouch = 0.25f;
    private bool isCrouching;
    private bool duringCrouchAnimation;
    private bool canCrouch = true;

    [Header("Stamina")]
    [SerializeField, Range(1, 20)] private float maxStamina = 10f;
    [SerializeField] private AudioSource windedAudioSource;
    float currentStamina;

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
            else if(isCrouching)
            {
                currentSpeed = crouchSpeed;
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
            /*
            if(canCrouch)
            {
                AttemptToCrouch();
            }
            */
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
        UpdateStamina();
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
    public void AttemptToCrouch()
    {
        if(!duringCrouchAnimation && controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(CrouchOrStand());
            }
        }
    }

    private IEnumerator CrouchOrStand()
    {
        duringCrouchAnimation = true;

        float timeElapsed = 0f;
        float currentHeight = controller.height;
        float targetHeight;
        if(isCrouching)
        {
            targetHeight = standingHeight;
            isCrouching = false;
        }
        else
        {
            targetHeight = crouchHeight;
            isCrouching = true;
        }

        while(timeElapsed > timeToCrouch)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        controller.height = targetHeight;

        duringCrouchAnimation = false;
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
        Debug.Log("Stamina: " + currentStamina);
    }
}


