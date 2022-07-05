using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject cursorUI;
    [SerializeField] private GameObject journalUIOne;
    [SerializeField] private GameObject journalUITwo;
    [SerializeField] private GameObject quitGameOptionUI;
    [SerializeField] private LevelController levelController;

    [Header ("Main Menu Only")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject invisibleFullscreenButton;
    [SerializeField] private GameObject garageObject;
    [SerializeField] private GameObject mainMenuExposition;
    [SerializeField] private GameObject expositionTextOne;
    [SerializeField] private GameObject expositionTextTwo;
    [SerializeField] private GameObject expositionTextThree;
    [SerializeField] private GameObject expositionTextFour;
    [SerializeField] private GameObject playGameButton;
    [SerializeField] private float textDelayTimer;
    public static bool gamePaused = false;
    private bool canPauseGame = false;
    private bool canClickToContinue = false;
    private int currentText;

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
        if(canClickToContinue)
        {
            invisibleFullscreenButton.SetActive(true);
        }
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
    public void GoToMainMenu()
    {
        levelController.LoadLevel(0);
    }

    public void EnableMainMenuExposition()
    {
        garageObject.SetActive(false);
        mainMenuUI.SetActive(false);

        mainMenuExposition.SetActive(true);
        StartCoroutine(DelayClickTimer());
        expositionTextOne.SetActive(true);
        currentText = 0;
    }

    
    //If "flip journal page" button clicked while reading journal
    public void EnableExpositionNoteTwo()
    {
        journalUIOne.SetActive(false);
        journalUITwo.SetActive(true);
    }

    //If "close journal" button clicked while reading journal
    public void DisableExpositionJournal()
    {
        journalUIOne.SetActive(false);
        journalUITwo.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        cursorUI.SetActive(true);
        AudioListener.volume = 1f;
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor to center of window
        Time.timeScale = 1f;
        gamePaused = false;
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        cursorUI.SetActive(false);
        AudioListener.volume = 0;
        Cursor.lockState = CursorLockMode.None; //Unlock cursor
        Time.timeScale = 0f;
        gamePaused = true;
    }

    // Invisible button to move to next exposition
    public void ProgressExpositionText()
    {
        invisibleFullscreenButton.SetActive(false);
        if(currentText == 0)
        {
            expositionTextOne.SetActive(false);
            expositionTextTwo.SetActive(true);
            StartCoroutine(DelayClickTimer());
            currentText = 1;
        }
        else if(currentText == 1)
        {
            expositionTextTwo.SetActive(false);
            expositionTextThree.SetActive(true);
            StartCoroutine(DelayClickTimer());
            currentText = 2;
        }
        else if(currentText == 2)
        {
            expositionTextThree.SetActive(false);
            expositionTextFour.SetActive(true);
            canClickToContinue = false;
            playGameButton.SetActive(true);
        }
    }

    private IEnumerator DelayClickTimer()
    {
        canClickToContinue = false;
        yield return new WaitForSeconds(textDelayTimer);
        canClickToContinue = true;
    }

    

    public void EnableQuitGameOption()
    {
        quitGameOptionUI.SetActive(true);
    }
    public void DeclineQuitGame()
    {
        quitGameOptionUI.SetActive(false);
    }
    public void ConfirmQuitGame()
    {
        Application.Quit();
    }
}
