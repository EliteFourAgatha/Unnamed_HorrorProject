using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private AudioSource thunderAudioSource;
    bool canPlayAudio = true;
    [SerializeField] private int[] waitTimes = {120, 180, 240};
    void Awake()
    {
        if(thunderAudioSource == null)
        {
            thunderAudioSource = gameObject.GetComponent<AudioSource>();
        }
    }
    void Update()
    {
        if(canPlayAudio)
        {
            thunderAudioSource.Play();
            canPlayAudio = false;
            WaitToPlayAudio();
        }
    }
    IEnumerator WaitToPlayAudio()
    {
        int randIndex = Random.Range(0, waitTimes.Length);
        int randTime = waitTimes[randIndex];
        yield return new WaitForSeconds(randTime);
        canPlayAudio = true;
    }
}
