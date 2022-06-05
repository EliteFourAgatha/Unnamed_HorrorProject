using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject endingUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject mainMenuExposition;
    [SerializeField] private GameObject expositionUIOne;
    [SerializeField] private GameObject expositionUITwo;
    [SerializeField] private GameObject quitGameOptionUI;
    [SerializeField] private LevelController levelController;
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

    //Main Menu: Play Button
    public void MenuPlayButton()
    {
        EnableMainMenuExposition();
    }
    public void GoToCreditsMenu()
    {
        endingUI.SetActive(false);
        creditsUI.SetActive(true);
    }
    public void GoToMainMenu()
    {
        levelController.LoadLevel(0);
    }

    public void EnableMainMenuExposition()
    {
        levelController.FadeToBlack();
        mainMenuExposition.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void DisableExpositionNoteOne()
    {
        expositionUIOne.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void DisableExpositionNoteTwo()
    {
        expositionUITwo.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //
    // -- PAUSE MENU --
    //
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        AudioListener.volume = 1f;
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor to center of window
        Time.timeScale = 1f;
        gamePaused = false;
    }
    public void PauseGame()
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
