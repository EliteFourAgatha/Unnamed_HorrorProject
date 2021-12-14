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
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        AudioListener.volume = 1f;
        //AudioListener.pause = false; //Unpause all current audio
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        AudioListener.volume = 0;
        //AudioListener.pause = true; //Pause all current audio
        Cursor.lockState = CursorLockMode.None; //Unlock cursor
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }    
    public IEnumerator ShowPopupMessage(string message, float delay)
    {
        popupText.text = message;
        popupText.enabled = true;
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeOutPopupText(popupText, 2f));
    }
    private IEnumerator FadeOutPopupText(Text popupText, float fadeTime)
    {
        float elapsedTime = 0.0f;
        Color textColor = popupText.color;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            popupText.color = textColor;
        }
        if(popupText.color.a == 0)
        {
            popupText.enabled = false;
        }
        yield return null;
    }
}
