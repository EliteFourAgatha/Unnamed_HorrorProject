using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject optionsMenuUI;
    public GameObject pauseMenuUI;
    public GameObject mainMenuUI;
    public GameObject expositionUI;
    public GameObject controlMenuUI;
    public GameObject quitGameOptionUI;
    public GameObject pressEnterButton;
    public GameObject expositionText;
    public LevelController levelController;
    public static bool gamePaused = false;
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }
    // -- MAIN MENU BUTTONS --
    //Main Menu: Play Button
    public void MenuPlayButton()
    {
        EnableExpositionScreen();
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
    // -- END MAIN MENU --

    // -- EXPOSITION SCREEN, AFTER 'PLAY' IN MAIN MENU --
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
    public void CheckExpositionScreenInput()
    {
        if(expositionActive)
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            {
                pressEnterButton.SetActive(true);
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
    // -- END EXPOSITION --

    // -- CONTROL MENU, FROM MAIN / PAUSE MENUS --
    public void EnableControlMenu()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "MainMenu")
        {
            mainMenuUI.SetActive(false);
        }
        else
        {
            pauseMenuUI.SetActive(false);
        }
        controlMenuUI.SetActive(true);
    }
    public void DisableControlMenu()
    {
        Scene scene = SceneManager.GetActiveScene();
        controlMenuUI.SetActive(false);
        if(scene.name == "MainMenu")
        {
            mainMenuUI.SetActive(true);
        }
        else
        {
            pauseMenuUI.SetActive(true);
        }
    }
    // -- END CONTROL MENU --

    // -- PAUSE MENU --
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        AudioListener.volume = 1f;
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor
        Cursor.visible = false;
        Time.timeScale = 1f;
        gamePaused = false;
    }
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        AudioListener.volume = 0;
        Cursor.lockState = CursorLockMode.None; //Unlock cursor
        Cursor.visible = true;
        Time.timeScale = 0f;
        gamePaused = true;
    }
    //Pause Menu: Quit Button
    public void EnableQuitGameOption()
    {
        quitGameOptionUI.SetActive(true);
    }    
    //Quit option: No
    public void DeclineQuitGame()
    {
        quitGameOptionUI.SetActive(false);
    }
    //Quit option: Yes
    public void ConfirmQuitGame()
    {
        Application.Quit();
    }
    // -- END PAUSE MENU --
}
