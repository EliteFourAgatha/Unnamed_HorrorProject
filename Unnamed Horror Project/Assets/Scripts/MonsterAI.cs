using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterAI : MonoBehaviour
{
    public Transform[] waypoints;
    private int destinationIndex = 0;

    public GameObject player;
    private NavMeshAgent agent;
    public LevelController levelController;
    public FlashlightToggle flashlightToggle;
    [SerializeField] private float catchRange = 5f;
    [SerializeField] private float flashlightRange = 10f;
    [SerializeField] private Transform[] levelTwoSpawnPoints;
    public bool awareOfPlayer = false;
    public bool playerIsHiding = false;
    public AudioClip chaseAudio;
    public AudioClip catchAudio;
    float distance;
    AudioSource audioSource;

    Scene currentScene;
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        audioSource = gameObject.GetComponent<AudioSource>();
        currentScene = SceneManager.GetActiveScene();

        GoToNextPatrolPoint();

        //If level 2,
        //TeleportNearPlayer(); ??
    }
    void Update()
    {
        /*
        if(awareOfPlayer)
        {
            Debug.Log("seen!");
        }
        else
        {
            Debug.Log("hidden!");
        }
        */
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

            // I think current best idea is:
            // Player caught, monster disappears / dissolves
            //  Dementor sound? Pleasure / ecstasy as he steals your vital essence?
            //  player caught first time: crippled, can't run, screen distorts. heartbeat plays?
            //  caught 2nd time: main menu or restart dungeon area



            //send player to main menu? (tedious, have to replay all previous sections)
            //send player to beginning of level 2? (low stakes, getting caught needs to be scarier)
            //some sort of web? player is caught first time, lose sanity / loses vision / crippled / can no longer run?
            //  2nd time caught: player given actual consequence (main menu, level 2, etc.)
            //
            // - or - player loses a bit of sanity each time he is caught
            //  as if a bit of his essence / soul being lost bit by bit
            //   savored by dark entity? this is how it feeds?
            //    if caught enough times, collapse and die?
            //     each time caught, vision blurrier / move speed slower?
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
    void TeleportNearPlayer()
    {
        //1.) DetermineClosestSpawnPoint
        //cycle through array of spawn points (shouldnt be too many)
        // spawn to point that is acceptable distance away from player
        //  (not too far away, not too close)

        //2.) SpawnMonster
        //spawn at chosen spawn point from above
        //  Make sure not in player line of sight?
        //  -or-
        //  Make all spawn points behind walls / obscured like in L4D?
        
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
