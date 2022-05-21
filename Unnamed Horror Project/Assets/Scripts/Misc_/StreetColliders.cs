using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetColliders : MonoBehaviour
{
    private string stopTravelString = "Not crazy about walking in this rain...";
    GameController gameController;
    private void Awake()
    {
        if(gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(gameController.ShowPopupMessage(stopTravelString, 2));
        }
    }
}
