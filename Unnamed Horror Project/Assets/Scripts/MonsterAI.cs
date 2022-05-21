using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int destinationIndex = 0;

    [SerializeField] private GameObject player;
    private NavMeshAgent agent;
    [SerializeField] private LevelController levelController;
    [SerializeField] private FlashlightToggle flashlightToggle;
    [SerializeField] private float catchRange = 5f;
    [SerializeField] private float flashlightRange = 10f;
    public bool awareOfPlayer = false;
    public bool playerIsHiding = false;
    [SerializeField] AudioClip chaseAudio;
    [SerializeField] AudioClip catchAudio;
    float distance;
    AudioSource audioSource;

    Scene currentScene;
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        audioSource = gameObject.GetComponent<AudioSource>();
        currentScene = SceneManager.GetActiveScene();

        GoToNextPatrolPoint();
    }
    void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if(playerIsHiding)
        {
            awareOfPlayer = false;
            audioSource.Stop();
        }
        if(currentScene.name == "Scene1")
        {
            CheckForFlashlight(distance);
        }

        if(awareOfPlayer)
        {
            audioSource.clip = chaseAudio;
            audioSource.loop = true;
            FollowPlayer();
            AttemptToCatchPlayer();
        }
        else
        {
            //Choose next point when agent gets close enough to current target
            if(!agent.pathPending && agent.remainingDistance <= 0.5f)
            {
                GoToNextPatrolPoint();
            }
        }
    }
    void FollowPlayer()
    {
        agent.destination = player.transform.position;
        Debug.Log("chasing");
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }


        //If player not seen again for x number of seconds, disappear?
        //  Can't chase forever, would be too much tension. Need to ebb and flow.
    }
    void AttemptToCatchPlayer()
    {
        if(distance <= catchRange)
        {
            Debug.Log("caught");
            /*
            audioSource.clip = catchAudio;
            audioSource.loop = false;
            audioSource.Play();
            if(!audioSource.isPlaying)
            {
                if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scene1"))
                {
                    levelController.FadeInToLevel(0);
                }
            }
            */
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
}
