using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject lightGO; //light gameObject to work with
    public Light lightOuter;
    public Light lightMiddle;
    public Light lightInner;
    private bool lightOn = false;
    private float batteryLevel;
    void Start()
    {
        batteryLevel = 100;
    }
    void Update()
    {
        ToggleFlashlight();
        UpdateBatteryLevel();
        ChangeLightIntensity();
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
    void UpdateBatteryLevel()
    {
        if(lightOn)
        {
            batteryLevel -= 1 * Time.deltaTime;
            Debug.Log(batteryLevel);
        }
    }
    void ChangeLightIntensity()
    {
        if(batteryLevel <= 100 && batteryLevel > 75)
        {
            lightOuter.intensity = 10f;
            lightMiddle.intensity = 8f;
            lightInner.intensity = 6f;
        }
        else if(batteryLevel <= 75 && batteryLevel > 50)
        {
            lightOuter.intensity = 8f;
            lightMiddle.intensity = 6f;
            lightInner.intensity = 4f;
        }
        else if(batteryLevel <= 50 && batteryLevel > 25)
        {
            lightOuter.intensity = 6f;
            lightMiddle.intensity = 4f;
            lightInner.intensity = 2f;            
        }
        else if(batteryLevel <= 25 && batteryLevel > 0)
        {
            lightOuter.intensity = 4f;
            lightMiddle.intensity = 2f;
            lightInner.intensity = 0f;            
        }
        else if(batteryLevel == 0)
        {
            //battery dead animation?
            lightOuter.intensity = 0f;
            lightMiddle.intensity = 0f;
            lightInner.intensity = 0f;
        }
    }
}
