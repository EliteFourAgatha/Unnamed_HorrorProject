using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Animator animator;
    private int levelToLoad;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void FadeInToLevel(int levelNumber)
    {
        levelToLoad = levelNumber;
        animator.SetTrigger("FadeIn");
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
