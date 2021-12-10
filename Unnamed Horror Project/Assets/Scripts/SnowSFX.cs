using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSFX : MonoBehaviour
{
    AudioSource audioSource;
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
