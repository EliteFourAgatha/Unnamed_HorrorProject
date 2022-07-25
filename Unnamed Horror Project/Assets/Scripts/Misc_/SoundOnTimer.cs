using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnTimer : MonoBehaviour
{
    [SerializeField] private AudioClip[] woodCreakClips;
    [SerializeField] private AudioClip[] buildingGroanClips;
    [SerializeField] private AudioClip[] thunderClips;
    [SerializeField] private AudioClip[] monsterWheezeClips;
    [SerializeField] private AudioSource audioSource;
    

    [SerializeField] private int[] thunderWaitTimes = {120, 150, 180};
    [SerializeField] private int[] woodWaitTimes = {15, 25, 35};
    [SerializeField] private int[] wheezeWaitTimes = {10, 15, 20};
    public enum SoundType {Thunder, WoodGroaning, MonsterWheeze}
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
        int clipIndex;
        int timerIndex;
        int waitTime = 0;

        if(soundType == SoundType.Thunder)
        {
            clipIndex = Random.Range(0, thunderClips.Length);
            audioSource.clip = thunderClips[clipIndex];

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
        else if(soundType == SoundType.MonsterWheeze)
        {
            clipIndex = Random.Range(0, monsterWheezeClips.Length);
            audioSource.clip = monsterWheezeClips[clipIndex];

            timerIndex = Random.Range(0, wheezeWaitTimes.Length);
            waitTime = wheezeWaitTimes[timerIndex];
        }

        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        yield return new WaitForSeconds(waitTime);
        canPlayAudio = true;
    }
}
