using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private GameObject player;
    [SerializeField] private LevelController levelController;
    [SerializeField] private FlashlightToggle flashlightToggle;
    [SerializeField] private float catchRange = 5f;
    [SerializeField] private float flashlightRange = 10f;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip catchAudio;
    [SerializeField] private Transform monsterResetLocation;
    [SerializeField] private Transform playerResetLocation;
    [SerializeField] private Light[] disabledBasementLights;
    [SerializeField] private Light backroomLightOne;
    [SerializeField] private Light backroomLightTwo;

    public bool awareOfPlayer = false;
    public bool playerIsHiding = false;
    public bool playerCanHide = true;
    
    private NavMeshAgent agent;
    private float distance;
    private AudioSource audioSource;
    private int destinationIndex = 0;
    private bool monsterCanMove = true;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        audioSource = gameObject.GetComponent<AudioSource>();

        GoToNextPatrolPoint();
    }
    void Update()
    {
        if(monsterCanMove)
        {
            distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            if(distance <= 6)
            {
                playerCanHide = false;
                awareOfPlayer = true;
            }
            else
            {
                playerCanHide = true;
            }
            if(playerIsHiding)
            {
                awareOfPlayer = false;
                audioSource.Stop();
            }
            CheckForFlashlight(distance);

            if(awareOfPlayer)
            {
                agent.speed = 3.5f;
                FollowPlayer();
                AttemptToCatchPlayer();
            }
            else
            {
                agent.speed = 2f;
                //Choose next point when agent gets close enough to current target
                if(!agent.pathPending && agent.remainingDistance <= 0.5f)
                {
                    GoToNextPatrolPoint();
                }
            }
        }
    }
    void FollowPlayer()
    {
        agent.destination = player.transform.position;        
        
        //Instead of playing catch audio, music cuts out. Silence.
        musicAudioSource.Pause();
        
        // If outside of follow radius, start timer
        // if timer reaches x, stop following
        // if player back in radius, stop and reset timer value
    }
    void AttemptToCatchPlayer()
    {
        if(distance <= catchRange)
        {
            awareOfPlayer = false;
            StartCoroutine(RestartAtBasementCheckpoint());
        }
    }

    void CheckForFlashlight(float distance)
    {
        if(flashlightToggle.lightOn)
        {
            if(distance <= flashlightRange)
            {
                awareOfPlayer = true;
            }
        } 
    }

    //Get next waypoint in array and move agent there
    //From documentation: https://docs.unity3d.com/Manual/nav-AgentPatrol.html
    void GoToNextPatrolPoint()
    {
        agent.destination = waypoints[destinationIndex].position;

        //Increment index after movement
        destinationIndex += 1;

        //If index same as number of elements in array, reset to 0
        //(Roll over back to 0 when index is equal to (last array value +1))
        if(destinationIndex == waypoints.Length)
        {
            destinationIndex = 0;
        }
        Debug.Log("waypoint: " + destinationIndex);
    }
    private IEnumerator RestartAtBasementCheckpoint()
    {
        monsterCanMove = false;
        gameObject.transform.position = monsterResetLocation.position;
        player.transform.position = playerResetLocation.position;

        foreach(Light light in disabledBasementLights)
        {
            light.enabled = false;
        }
        backroomLightOne.enabled = true;
        backroomLightTwo.enabled = true;

        levelController.FadeToBlack();
        
        audioSource.clip = catchAudio;
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        yield return new WaitForSeconds(2f);
        //jump scare / monster appears in front of screen slowly and fades to black
        levelController.FadeInFromBlack();
        monsterCanMove = true;
    }
}
