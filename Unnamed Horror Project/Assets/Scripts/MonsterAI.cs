using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 5f;
    float maxDist = 10f;
    float minDist = 2f;
    float distance;
    void Start()
    {
        
    }
    void Update()
    {
        //Get basic follow logic from ai scripts from tank game.
        // Don't reinvent the wheel! copy as much as you can.
        //  Get used to modular components that can be reused
    }
    void BasicFollowLogic()
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        gameObject.transform.LookAt(player.transform);
        if(distance >= minDist)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
