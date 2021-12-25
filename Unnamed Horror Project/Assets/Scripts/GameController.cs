using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    DoorController doorScript;
    public static bool gameIsPaused = false;
    public GameObject player;
    public GameObject pauseMenuUI;
    public Text popupText;
    public Text objectiveText;
    public AudioClip carEndingSFX;
    private AudioSource audioSource;
    public LevelController levelController;
    public int currentCheckpoint;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if(levelController == null)
        {
            levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        }
    }
    private void Start()
    {
        //Lock cursor to center of game window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentCheckpoint = 0;
    }

    private void Update()
    {
        DetermineObjectiveText();
        if (Input.GetKeyDown(KeyCode.J))
        {
            levelController.FadeInToLevel(0);
            Debug.Log("fade to main menu");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    //
    //
    //MOVE TO MENU CONTROLLER
    //
    //
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        AudioListener.volume = 1f;
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    //
    //
    //MOVE TO MENU CONTROLLER
    //
    //
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        AudioListener.volume = 0;
        Cursor.lockState = CursorLockMode.None; //Unlock cursor
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void DetermineObjectiveText()
    {
        if(currentCheckpoint == 0)
        {
            objectiveText.text = "Get task list from head office";
        }
        else if(currentCheckpoint == 1)
        {
            objectiveText.text = "Turn off power breaker to storage area";
        }
        else if(currentCheckpoint == 2)
        {
            objectiveText.text = "Fix faulty light in storage room 4";
        }
    }
    public IEnumerator ShowPopupMessage(string message, float delay)
    {
        popupText.text = message;
        popupText.enabled = true;
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeOutPopupText(popupText, 4f));
    }
    private IEnumerator FadeOutPopupText(Text popupText, float fadeTime)
    {
        Color originalColor = popupText.color;
        for(float t = 0.01f; t < fadeTime; t += Time.deltaTime) //Per second, use deltaTime
        {
            popupText.color = Color.Lerp(originalColor, new Color(1, 1, 1, 0), 
                                            Mathf.Min(1, t/fadeTime));
        }
        popupText.enabled = false;
        yield return null;
    }
}
