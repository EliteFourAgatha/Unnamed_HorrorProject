using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private Animator animator;
    private int levelToLoad;
    public GameObject retryLevelOneScreen;    
    public GameObject retryLevelTwoScreen;
    Scene currentScene;
    [SerializeField] private Image blackFadeImage;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Scene1")
        {
            animator.Play("Fade_In");
        }
    }
    public void LoadLevel(int levelNumber)
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(LoadLevelAfterDelay(levelNumber));
    }
    IEnumerator LoadLevelAfterDelay(int levelNumber)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelNumber);
    }
    public void FadeToBlack()
    {
        if(!blackFadeImage.enabled)
        {
            blackFadeImage.enabled = true;
        }
        animator.Play("Fade_Out");
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
