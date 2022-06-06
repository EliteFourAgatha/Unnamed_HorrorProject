using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashlightToggle : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject lightGO; //light gameObject to work with
    [SerializeField] private AudioSource flashlightAudioSource;
    public bool lightOn = false;
    Scene currentScene;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }
    void ToggleFlashlight()
    {
        if(lightOn)
        {
            lightGO.SetActive(false);
            lightOn = false;
            if(!flashlightAudioSource.isPlaying)
            {
                flashlightAudioSource.Play();
            }
        }
        else
        {
            lightGO.SetActive(true);
            lightOn = true;
            if(!flashlightAudioSource.isPlaying)
            {
                flashlightAudioSource.Play();
            }
        }
}
}
