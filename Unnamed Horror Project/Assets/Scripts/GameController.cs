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
    private LevelController levelController;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        levelController = gameObject.GetComponent<LevelController>();
    }
    private void Start(){
        //Lock cursor to center of game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update(){
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
    public void ResumeGame(){
        pauseMenuUI.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        AudioListener.volume = 1f;
        //AudioListener.pause = false; //Unpause all current audio
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void PauseGame(){
        pauseMenuUI.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
        AudioListener.volume = 0;
        //AudioListener.pause = true; //Pause all current audio
        Cursor.lockState = CursorLockMode.None; //Unlock cursor
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void QuitGame(){
        Application.Quit();
    }    
    public IEnumerator ShowMessage(string message, float delay)
    {
        popupText.text = message;
        popupText.text.enabled = true;
        yield return new WaitForSeconds(delay);
        popupText.text.enabled = false;
    }
}
