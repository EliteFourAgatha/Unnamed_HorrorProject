using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Animator animator;
    private int levelToLoad;
    public GameObject retryLevelOneScreen;    
    public GameObject retryLevelTwoScreen;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void FadeInToLevel(int levelNumber)
    {
        levelToLoad = levelNumber;
        animator.SetTrigger("FadeIn");
        animator.SetTrigger("FadeOut");
        Cursor.lockState = CursorLockMode.Locked;
    }
    /*
    public void FadeToBlack()
    {
        animator.SetTrigger("FadeIn");
    }
    */

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void EnableRetryScreen(int sceneNum)
    {
        if(sceneNum == 1)
        {
            retryLevelOneScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            retryLevelTwoScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
