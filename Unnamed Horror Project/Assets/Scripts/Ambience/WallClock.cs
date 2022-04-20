using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //for DateTime and TimeSpan

public class WallClock : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform secondHand;
    private const float secondsToDegrees = 360f/60f;
    TimeSpan timeSpan;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        AnimateMinuteHand();
    }
    public void AnimateMinuteHand()
    {
        timeSpan = DateTime.Now.TimeOfDay;
        //Multiply by negative secondsToDegrees because we're looking down Z-axis
        secondHand.localRotation = Quaternion.Euler(
            0f, 0f, (float)timeSpan.TotalSeconds * secondsToDegrees
        );
    }
}
