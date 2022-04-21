using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    public GameController gameController;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float maxTimer = 1.0f;
    [SerializeField] private Image radialImage;
    [SerializeField] private GameObject player;
    private bool canUpdate = true;
    private bool canInteract = false;
    //put script on empty object that is where you want player to interact
    // reference ui object here instead of having this script on ui object
    //  if distance between object and player is small enough, canInteract
    private float distance;
    public enum RadialType{Window, Sink, TV, BloodChalice};
    public RadialType radialType;
    void Update()
    {
        if(radialType == RadialType.Sink)
        {
            if(Vector3.Distance(gameObject.transform.position, player.transform.position) <= 3f)
            {
                if(canUpdate)
                {
                    radialImage.enabled = true;
                    UpdateRadialFill();
                }

            }

        }
        if(canInteract)
        {
            radialImage.enabled = true;
            if(radialType == RadialType.Sink)
            {
                if(gameController.currentCheckpoint == 4)
                {
                    UpdateRadialFill();
                }
            }
            else if(radialType == RadialType.Window)
            {
                if(gameController.currentCheckpoint == 2)
                {
                    UpdateRadialFill();
                }
            }
            else if(radialType == RadialType.TV)
            {
                if(gameController.currentCheckpoint == 6)
                {
                    UpdateRadialFill();
                }
            }
        }
    }
    private void UpdateRadialFill()
    {
        if(Input.GetKey(KeyCode.E))
        {
            timer += Time.deltaTime;
            radialImage.fillAmount = timer;

            if(timer >= maxTimer)
            {
                timer = 0;
                radialImage.fillAmount = 0;
                radialImage.enabled = false;
                canUpdate = false;
                Debug.Log("done?");
                //fire function here to move on to next task
            }
        }
    }
}
