using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private bool isFlickering = false;
    public float flickerDelay;
    public Light lightToFlicker;
    public MeshRenderer lightFoundation;
    private Material lightFoundationMat;
    private Color normalColor;
    void Awake()
    {
        if(lightToFlicker == null)
        {
            lightToFlicker = gameObject.GetComponent<Light>();
        }
        lightFoundationMat = lightFoundation.material;
        normalColor = lightFoundationMat.color;
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
        lightFoundationMat.SetColor("_EmissionColor", Color.black);
        flickerDelay = Random.Range(0.5f, 2f);
        yield return new WaitForSeconds(flickerDelay);
        //Enable light, then randomize delay time and wait
        lightToFlicker.enabled = true;
        lightFoundationMat.SetColor("_EmissionColor", normalColor);
        flickerDelay = Random.Range(0.01f, 0.5f);
        yield return new WaitForSeconds(flickerDelay);
        isFlickering = false;
    }
}
