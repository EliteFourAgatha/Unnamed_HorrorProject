using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private Animator animator;
    Scene currentScene;
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Start()
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
        animator.Play("Fade_Out");
    }
    public void FadeInFromBlack()
    {
        animator.Play("Fade_In");
    }
}
