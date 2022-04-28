using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterAI : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public LevelController levelController;
    public FlashlightToggle flashlightToggle;
    public AudioSource audioSource;
    [SerializeField] private float catchRange = 5f;
    [SerializeField] private float flashlightRange = 10f;
    [SerializeField] private Transform[] levelTwoSpawnPoints;
    private bool playerInLineOfSight = false;
    public bool awareOfPlayer = true;
    float distance;
    bool playerIsHiding;
    Scene currentScene;
    void Start()
    {
        if(agent == null)
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
        }
        currentScene = SceneManager.GetActiveScene();
        TeleportNearPlayer();
    }
    void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if(playerIsHiding)
        {
            awareOfPlayer = false;
        }
        if(currentScene.name == "Scene1")
        {
            CheckForFlashlight(distance);
        }

        

        //Create a trigger collider that acts as a "line of sight" cone.
        //  If player collides with this cone, player is seen. Monster behavior changes.

        if(awareOfPlayer)
        {
            FollowPlayer();
            AttemptToCatchPlayer();
        }
        else
        {
            PatrolArea();
        }
    }
    void FollowPlayer()
    {
        agent.destination = player.transform.position;

        //If player not seen again for x number of seconds, disappear?
        //  Can't chase forever, would be too much tension. Need to ebb and flow.
    }
    void AttemptToCatchPlayer()
    {
        if(distance <= catchRange)
        {
            audioSource.Play();
            if(!audioSource.isPlaying)
            {
                if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Scene1"))
                {
                    levelController.FadeInToLevel(0);
                }

            }

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
    void PatrolArea()
    {

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

    //Start patrolling at waypoint closest to spawn point
    void DetermineFirstPatrolpoint()
    {
        //cycle through array of patrol waypoints
        // find one closest to gameObject.transform.position
    }
}
