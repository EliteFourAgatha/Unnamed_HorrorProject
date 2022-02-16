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
    private bool canPauseGame = false;

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
    }
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;
        if(buildIndex == 0)
        {
            canPauseGame = false;
        }
        else
        {
            canPauseGame = true;
        }
        if(canPauseGame)
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
    }
    public void StartGameFromExposition()
    {
        expositionUI.SetActive(false);
        levelController.FadeInToLevel(1);
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
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor to center of window
        Time.timeScale = 1f;
        gamePaused = false;
    }
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        AudioListener.volume = 0;
        Cursor.lockState = CursorLockMode.None; //Unlock cursor
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
