using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSound : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] dripSounds;

    void Update()
    {
        if(_particleSystem.time <= 1.25f && _particleSystem.time >= 0.75f)
        {
            audioSource.clip = dripSounds[Random.Range(0, dripSounds.Length)];
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
