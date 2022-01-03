using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedSlider : MonoBehaviour
{
    public GameObject timeSlider;
    public LevelController levelController;
    Image timerBar;
    public float maxTime = 5f;
    float timeRemaining;
    void Start()
    {
        if(levelController == null)
        {
            levelController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        }
        timeRemaining = maxTime;
        timerBar = timeSlider.GetComponent<Image>();
    }
    void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerBar.fillAmount = timeRemaining / maxTime;
        }
        else
        {
            //player caught animation? game over screen?
            levelController.EnableRetryScreen(1);
            Debug.Log("hiding failed.");
        }
    }
}
