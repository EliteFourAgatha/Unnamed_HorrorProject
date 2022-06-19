using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSound : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] dripSounds;
    private int currentParticles = 0;

    void Update()
    {
        if(particleSystem.time <= 1.25f && particleSystem.time >= 0.75f)
        {
            audioSource.clip = dripSounds[Random.Range(0, dripSounds.Length)];
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
