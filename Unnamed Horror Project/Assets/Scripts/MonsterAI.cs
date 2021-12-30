using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public GameObject player;
    public LevelController levelController;
    public float moveSpeed = 5f;
    float maxDist = 10f;
    float minDist = 10f;
    public float teleportRange = 25f;
    public float catchRange = 2f;
    float distance;
    void Start()
    {
        
    }
    void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        //Get basic follow logic from ai scripts from tank game.
        // Don't reinvent the wheel! copy as much as you can.
        //  Get used to modular components that can be reused
        BasicFollowLogic();
        TeleportIntoRange();
        CatchPlayer();
    }
    void BasicFollowLogic()
    {
        gameObject.transform.LookAt(player.transform);
        if(distance >= minDist)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
    //If player gets too far away, teleport to waypoint near player's position
    void TeleportIntoRange()
    {
        if(distance >= teleportRange)
        {
            //1. get player position.
            //2. find nearest waypoint? raycast? radius check?
            //3. teleport to waypoint
            //3.a. only if player not looking in that direction
            //3.b. can't teleport too close, too obvious. just to keep in range.
        }
    }
    void CatchPlayer()
    {
        if(distance <= catchRange)
        {
            levelController.FadeInToLevel(2);



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
}
