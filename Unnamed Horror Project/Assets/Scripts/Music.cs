using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource musicAudioSource;
    void Awake()
    {
        musicAudioSource = gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicAudioSource.Play();
    }
    public void StopMusic()
    {
        musicAudioSource.Stop();
    }
    public void PauseMusic()
    {
        musicAudioSource.Pause();
    }
}
