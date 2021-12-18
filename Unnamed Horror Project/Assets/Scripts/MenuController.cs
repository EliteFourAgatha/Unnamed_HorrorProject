using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject optionsMenuUI;
    public GameObject mainMenuUI;
    public GameObject expositionUI;
    public GameObject controlMenuUI;
    public GameObject quitGameOptionUI;
    public GameObject pressEnterInstructions;
    public LevelController levelController;
    private bool expositionActive = false;
    private bool pressEnterEnabled = false;

    void Awake()
    {
        if (levelController == null)
        {
            levelController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Update()
    {
        //clean up out of update
        if(expositionActive)
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            {
                pressEnterInstructions.SetActive(true);
                pressEnterEnabled = true;
            }
        }
        if(pressEnterEnabled)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                expositionActive = false;
                expositionUI.SetActive(false);
                levelController.FadeInToLevel(1);
            }
        }
    }
    //Main Menu: Play Button
    public void MenuPlayButton()
    {
        EnableExpositionScreen();
    }
    //Pause Menu: Quit Button
    public void EnableQuitGameOption()
    {
        quitGameOptionUI.SetActive(true);
    }
    //Quit option: No Button
    public void DeclineQuitGame()
    {
        quitGameOptionUI.SetActive(false);
    }
    //Quit option: Yes Button
    // -also-
    //Main menu: Quit Button
    public void ConfirmQuitGame()
    {
        Application.Quit();
    }
    public void EnableOptionsMenu()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }
    public void DisableOptionsMenu()
    {
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
    public void EnableExpositionScreen()
    {
        expositionUI.SetActive(true);
        mainMenuUI.SetActive(false);
        expositionActive = true;
    }
    public void StartGameFromExposition()
    {
        expositionUI.SetActive(false);
        expositionActive = false;
        levelController.FadeInToLevel(1);
    }
    public void EnableControlMenu()
    {
        //Necessary??
        //
        //
        //mainMenuUI.SetActive(false);
        controlMenuUI.SetActive(true);
    }
    public void DisableControlMenu()
    {
        controlMenuUI.SetActive(false);
    }
    public void ScrollInRulesText()
    {
        
    }

}
