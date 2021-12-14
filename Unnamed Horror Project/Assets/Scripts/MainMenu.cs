using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenuUI;
    public GameObject MainMenuUI;
    public GameObject rulesMenuUI;
    public GameObject pressEnterInstructions;
    public LevelController levelController;
    private bool rulesMenuActive = false;
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
        if(rulesMenuActive)
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            {
                pressEnterInstructions.SetActive(true);
                pressEnterEnabled = true;
            }
        }
        if(pressEnterEnabled)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                rulesMenuActive = false;
                rulesMenuUI.SetActive(false);
                levelController.FadeInToLevel(1);
            }
        }
    }

    public void PlayButton(){
        EnableRulesMenu();
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
    public void EnableRulesMenu()
    {
        rulesMenuUI.SetActive(true);
        MainMenuUI.SetActive(false);
        rulesMenuActive = true;
    }
    public void ScrollInRulesText()
    {
        
    }

}
