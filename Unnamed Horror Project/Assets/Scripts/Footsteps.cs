using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip walkAudio;
    public AudioClip runAudio;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayFootstepAudio()
    {

    }
    public void PauseFootstepAudio()
    {

    }

    
}
