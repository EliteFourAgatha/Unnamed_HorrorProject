using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClock : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(!audioSource.isPlaying())
        {
            audioSource.Play();
        }

    }
}
