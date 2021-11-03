using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject player;

    public GameObject pauseMenuUI;
    private void Start(){
        //Lock cursor to center of game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        AudioListener.volume = 1f;
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
        AudioListener.volume = 0;
        Cursor.lockState = CursorLockMode.None; //Unlock cursor
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    /*
    public void LoadOptions(){
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }*/  
    public void QuitGame(){
        Application.Quit();
    }
}
