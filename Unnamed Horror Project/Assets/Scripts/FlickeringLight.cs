using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private bool isFlickering = false;
    public float flickerDelay;
    public Light lightToFlicker;
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
        flickerDelay = Random.Range(0.01f, 0.5f);
        yield return new WaitForSeconds(flickerDelay);
        //Enable light, then randomize delay time and wait
        lightToFlicker.enabled = true;
        flickerDelay = Random.Range(0.01f, 0.5f);
        yield return new WaitForSeconds(flickerDelay);
        isFlickering = false;
    }
}
