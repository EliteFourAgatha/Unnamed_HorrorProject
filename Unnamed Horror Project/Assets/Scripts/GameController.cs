using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text popupText;
    [SerializeField] private Text objectiveText;
    [SerializeField] private Text endingUIText;
    private AudioSource audioSource;
    public int currentCheckpoint;
    public bool playerNeedsKey = true;
    public bool playerHasFuse = false;
    public bool breakerOn = false;
    public bool playerHasToolBox = false;
    Scene currentScene;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        //Lock cursor to center of game window
        Cursor.lockState = CursorLockMode.Locked;
        currentCheckpoint = 0;
        currentScene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        if(currentScene.name == "Scene1")
        {
            DetermineObjectiveText();
        }
        else
        {
            objectiveText.text = "";
        }
    }
    public void DetermineObjectiveText()
    {
        if(currentCheckpoint == 0)
        {
            objectiveText.text = "Get task list from head office";
        }
        else if(currentCheckpoint == 1)
        {
            objectiveText.text = "Find extra fuse in back room";
        }
        else if(currentCheckpoint == 2)
        {
            objectiveText.text = "Restore power to the basement";
        }
        else if(currentCheckpoint == 3)
        {
            objectiveText.text = "Find the toolbox left for you in the back room";
        }
        else if(currentCheckpoint == 4)
        {
            objectiveText.text = "Fix the laundry room window";
        }
        else if(currentCheckpoint == 5)
        {
            objectiveText.text = "Repair leak in the bathroom sink";
        }
        else if(currentCheckpoint == 6)
        {
            objectiveText.text = "Fix TV in open storage room";
        }
        else if(currentCheckpoint == 7)
        {
            objectiveText.text = "Turn off breaker and lock up before you leave";
        }
    }
    public IEnumerator ShowPopupMessage(string message, float delay)
    {
        popupText.text = message;
        popupText.enabled = true;
        yield return new WaitForSeconds(delay);
        popupText.enabled = false;
    }
    /*
    private IEnumerator FadeOutPopupText(Text popupText, float fadeTime)
    {
        Color originalColor = popupText.color;
        for(float t = 0.01f; t < fadeTime; t += Time.deltaTime) //Per second, use deltaTime
        {
            popupText.color = Color.Lerp(originalColor, new Color(1, 1, 1, 0), 
                                            Mathf.Min(1, t/fadeTime));
        }
        popupText.enabled = false;
        yield return null;
    }
    */
}
