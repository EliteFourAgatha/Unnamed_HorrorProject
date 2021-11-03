using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnding : MonoBehaviour
{
    public AudioSource carAudio;
    private void OnMouseOver(){
        
    }
    //If player clicks on car, fade to black and play SFX
    void OnMouseDown(){
        
        if(!carAudio.isPlaying){
            carAudio.Play();
        }
    }
}
