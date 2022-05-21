using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private bool isFlickering = false;
    [SerializeField] private float flickerDelay;
    [SerializeField] private Light lightToFlicker;
    [SerializeField] private AudioSource flickerAudio;
    void Awake()
    {
        if(lightToFlicker == null)
        {
            lightToFlicker = gameObject.GetComponent<Light>();
        }
    }

    void Update()
    {
        if(isFlickering == false)
        {
            StartCoroutine(FlickerLight());
        }
    }
    
    IEnumerator FlickerLight()
    {
        isFlickering = true;
        //Disable light, then randomize delay time and wait
        lightToFlicker.enabled = false;
        flickerAudio.Pause();
        flickerDelay = Random.Range(0.5f, 2f);
        yield return new WaitForSeconds(flickerDelay);
        //Enable light, then randomize delay time and wait
        lightToFlicker.enabled = true;
        flickerAudio.Play();
        flickerDelay = Random.Range(0.01f, 0.5f);
        yield return new WaitForSeconds(flickerDelay);
        isFlickering = false;
    }
}
