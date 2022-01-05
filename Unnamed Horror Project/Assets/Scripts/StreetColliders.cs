using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetColliders : MonoBehaviour
{
    private string stopTravelString = "Better not stray too far...";
    private string stopTravelStringTwo = "Probably a bad idea...";
    private GameController gameController;
    private void Awake()
    {
        if(gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(gameController.ShowPopupMessage(stopTravelString, 2));
        }
    }
}
