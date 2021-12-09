using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessTrigger : MonoBehaviour
{
    public GameObject darknessWall;
    public GameObject closetLightBulb;
    public GameObject dungeonDoor;
    void Start()
    {
        
    }
    //If player enters darkness trigger, 3 things happen
    //1. darkness wall spawns behind player so they can't leave
    //2. light bulb light goes out


    //3. dungeon door spawns and fades into view (Or player can do reverse alpha fade
    // as they get closer to door?)
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            darknessWall.SetActive(true);
            closetLightBulb.SetActive(false);
            dungeonDoor.SetActive(true);
        }
    }
}
