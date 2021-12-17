using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenuUI;
    public GameObject mainMenuUI;
    public GameObject expositionUI;
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

    public void PlayButton()
    {
        EnableRulesMenu();
    }

    public void QuitButton()
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
    public void EnableRulesMenu()
    {
        expositionUI.SetActive(true);
        mainMenuUI.SetActive(false);
        expositionActive = true;
    }
    public void ScrollInRulesText()
    {
        
    }

}
