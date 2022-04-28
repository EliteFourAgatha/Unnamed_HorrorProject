using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlashlightToggle : MonoBehaviour
{
    public GameController gameController;
    public GameObject lightGO; //light gameObject to work with
    public bool lightOn = false;
    private string flashlightBrokenString = "The flashlight won't turn on..";
    Scene currentScene;
    void Update()
    {
        //consider moving this to function that simply disables flashlight once
        //  level 2 starts. Then simply plays message if player press F.
        //   Reduce update calls.
        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Scene1")
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                ToggleFlashlight();
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(gameController.ShowPopupMessage(flashlightBrokenString, 2));
            }
        }
    }
    void ToggleFlashlight()
    {
        if(lightOn)
        {
            lightGO.SetActive(false);
            lightOn = false;
        }
        else
        {
            lightGO.SetActive(true);
            lightOn = true;
        }
}
}
