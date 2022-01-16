using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject lightGO; //light gameObject to work with
    private bool lightOn = false;
    void Update()
    {
        ToggleFlashlight();
    }
    void ToggleFlashlight()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(!lightOn)
            {
                lightGO.SetActive(true);
                lightOn = true;
            }
            else
            {
                lightGO.SetActive(false);
                lightOn = false;
            }
        }
    }
}
