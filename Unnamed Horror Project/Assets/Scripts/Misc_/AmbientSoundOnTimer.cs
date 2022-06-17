using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundOnTimer : MonoBehaviour
{
    [SerializeField] private AudioClip thunderAudioClip;
    [SerializeField] private AudioClip[] woodCreakClips;
    [SerializeField] private AudioClip[] buildingGroanClips;
    [SerializeField] private AudioSource audioSource;
    

    [SerializeField] private int[] thunderWaitTimes = {120, 150, 180};
    [SerializeField] private int[] woodWaitTimes = {15, 25, 35};
    public enum SoundType {Thunder, WoodGroaning}
    public SoundType soundType;


    private bool canPlayAudio = true;

    void Awake()
    {
        if(audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
    }
    void Update()
    {
        if(canPlayAudio)
        {
            StartCoroutine(PlaySoundThenWait());
        }
    }
    private IEnumerator PlaySoundThenWait()
    {
        canPlayAudio = false;
        AudioClip chosenClip;
        int clipIndex;
        int timerIndex;
        int waitTime = 0;

        if(soundType == SoundType.Thunder)
        {
            audioSource.clip = thunderAudioClip;

            timerIndex = Random.Range(0, thunderWaitTimes.Length);
            waitTime = thunderWaitTimes[timerIndex];
        }
        else if(soundType == SoundType.WoodGroaning)
        {
            clipIndex = Random.Range(0, woodCreakClips.Length);
            audioSource.clip = woodCreakClips[clipIndex];

            timerIndex = Random.Range(0, woodWaitTimes.Length);
            waitTime = woodWaitTimes[timerIndex];
        }

        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        yield return new WaitForSeconds(waitTime);
        canPlayAudio = true;
    }
}
