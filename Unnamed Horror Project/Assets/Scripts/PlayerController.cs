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
    private float moveForward;
    private float moveSide;
    private float currentSpeed;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    private bool canRun = true;
    public int maxStamina = 100;
    private int staminaValue;
    private Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        footstepAudioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        staminaValue = 100;
    }
    private void Update()
    {
        //Player winded, play SFX
        if(staminaValue <= 0)
        {
            canRun = false;
        }

        if(Input.GetKey("space"))
        {
            if(canRun)
            {
                currentSpeed = sprintSpeed;
                staminaValue --;
            }
        }
        else
        {
            currentSpeed = walkSpeed;
            if(staminaValue != maxStamina)
            {
                staminaValue ++;
            }
        }
        moveSide = Input.GetAxis("Horizontal") * currentSpeed;
        moveForward = Input.GetAxis("Vertical") * currentSpeed;
        Debug.Log(staminaValue);
        
        //If player not moving
        if(rigidbody.velocity.z == 0 && rigidbody.velocity.x == 0)
        {
            Debug.Log("player not moving");
            PauseFootstepAudio();
            staminaValue++;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    public void MovePlayer()
    {
        rigidbody.velocity = (transform.forward * moveForward) + (transform.right * moveSide);
        PlayFootstepAudio();
        //Vector3 move = transform.right * x + transform.forward * z;
        //controller.Move(move * speed * Time.deltaTime);

    }
    public void HandleStairs()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        Vector3 targetPosition;
        targetPosition = transform.position;

        //if(Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        //{

        //}
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


