using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenuUI;
    public GameObject MainMenuUI;

    public void PlayButton(){
        SceneManager.LoadScene(1);
    }

    public void QuitButton(){
        Application.Quit();
    }

    public void EnableOptionsMenu(){
        MainMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(true);
    }
    public void DisableOptionsMenu(){
        OptionsMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

}
