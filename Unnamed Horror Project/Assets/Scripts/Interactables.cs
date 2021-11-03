using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placed on player
// Used to check trigger zones and interact with doors, items, etc.
public class Interactables : MonoBehaviour
{
    [SerializeField] private DoorController doorScript;
    [SerializeField] private GameController gameController;
    private Animator anim;
    private void Awake(){
        anim = gameObject.GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other){
        if(Input.GetKeyDown(KeyCode.E)){
            if(other.gameObject.tag == "Door"){
            }
        }

    }
}
