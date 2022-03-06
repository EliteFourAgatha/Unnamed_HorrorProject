using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text popupText;
    public Text objectiveText;
    private AudioSource audioSource;
    public int currentCheckpoint;
    public bool playerHasKey = false;
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        //Lock cursor to center of game window
        Cursor.lockState = CursorLockMode.Locked;
        currentCheckpoint = 0;
    }
    private void Update()
    {
        DetermineObjectiveText();
    }
    public void DetermineObjectiveText()
    {
        if(currentCheckpoint == 0)
        {
            objectiveText.text = "Get task list from head office";
        }
        else if(currentCheckpoint == 1)
        {
            objectiveText.text = "Close window in laundry room";
        }
        else if(currentCheckpoint == 2)
        {
            objectiveText.text = "Find wrench in maintenance room";
        }
        else if(currentCheckpoint == 3)
        {
            objectiveText.text = "Fix leak in the bathroom";
        }
        else if(currentCheckpoint == 4)
        {
            objectiveText.text = "Turn off power breaker to storage area";
        }
        else if(currentCheckpoint == 5)
        {
            objectiveText.text = "Fix faulty light in storage room 2";
        }
        else if(currentCheckpoint == 6)
        {
            objectiveText.text = "Investigate maintenance room";
        }
    }
    public IEnumerator ShowPopupMessage(string message, float delay)
    {
        popupText.text = message;
        popupText.enabled = true;
        yield return new WaitForSeconds(delay);
        popupText.enabled = false;
        //StartCoroutine(FadeOutPopupText(popupText, 4f));
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
