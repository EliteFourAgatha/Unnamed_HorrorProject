using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public GameObject player;
    public LevelController levelController;
    public float moveSpeed = 5f;
    //float maxDist = 10f;
    float minDist = 10f;
    public float teleportRange = 25f;
    public float catchRange = 2f;
    private bool playerSeen = false;
    float distance;
    void Start()
    {
        TeleportIntoQuadrant();
    }
    void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        //Get basic follow logic from ai scripts from tank game.
        // Don't reinvent the wheel! copy as much as you can.
        //  Get used to modular components that can be reused


        //Create a trigger collider that acts as a "line of sight" cone.
        //  If player collides with this cone, player is seen. Monster behavior changes.

        if(playerSeen)
        {
            FollowPlayer();
            AttemptToCatchPlayer();
        }
        else
        {
            PatrolCurrentQuadrant();
        }
    }
    void FollowPlayer()
    {
        gameObject.transform.LookAt(player.transform);
        if(distance >= minDist)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        //If player not seen again for x number of seconds, disappear?
        //  Can't chase forever, would be too much tension. Need to ebb and flow.
    }
    void TeleportIntoQuadrant()
    {
        //Call function here that gets current quadrant of player.
        // Example: If player is in red lights room, quadrant = 1. Teleport
        //  monster to quadrant 1, and start at waypoint 1.
        

        //if playerquadrant = 1,
        //  transform.position = quadrant1waypoint1
    }
    void AttemptToCatchPlayer()
    {
        if(distance <= catchRange)
        {
            levelController.FadeInToLevel(3);
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
    void PatrolCurrentQuadrant()
    {

    }
}
