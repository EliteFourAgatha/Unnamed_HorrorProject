using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    DoorController doorScript;
    //Static allows you to keep track of variable status regardless
    public static bool gameIsPaused = false;
    public GameObject player;
    public GameObject pauseMenuUI;
    public Animator fadeAnimator;
    public AudioClip carEndingSFX;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
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
    /*
    public void LoadOptions(){
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }*/
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

    public void FadeOutToBlack()
    {
        
    }
    public void FadeInFromBlack()
    {

    }

    public void StartCarEnding()
    {
        audioSource.clip = carEndingSFX;
        audioSource.Play();
        FadeOutToBlack();
        //Wait for fade out to finish
        
        //Pause game (timescale = 0), then fade/scroll-in text for endings

        SceneManager.LoadScene(0); //Load main menu

        //Keep track of car ending having been found (2/4 endings found)
    }
}
