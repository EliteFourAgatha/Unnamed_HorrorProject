using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedSlider : MonoBehaviour
{
    public GameObject timeSlider;
    Image sliderImage;
    public float maxTime = 5f;
    float timeRemaining;
    void Start()
    {
        timeRemaining = maxTime;
        sliderImage = timeSlider.GetComponent<Image>();
    }
    void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            sliderImage.fillAmount = timeRemaining / maxTime;
            Debug.Log(timeRemaining);
        }
        else
        {
            //player caught animation? game over screen?
            Debug.Log("hiding failed.");
        }
    }
}
