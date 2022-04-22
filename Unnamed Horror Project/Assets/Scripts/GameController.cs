using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text popupText;
    public Text objectiveText;
    public Text endingUIText;
    private AudioSource audioSource;
    public int currentCheckpoint;
    public bool playerNeedsKey = true;
    public bool breakerOn = false;
    public bool playerHasPliers = false;
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
            objectiveText.text = "Turn on the power breaker";
        }
        else if(currentCheckpoint == 2)
        {
            objectiveText.text = "Fix the laundry room window and make sure it stays closed.";
        }
        else if(currentCheckpoint == 3)
        {
            objectiveText.text = "Find the pliers left for you in the back room";
        }
        else if(currentCheckpoint == 4)
        {
            objectiveText.text = "Repair leak in the bathroom sink";
        }
        else if(currentCheckpoint == 5)
        {
            objectiveText.text = "Fix TV in open storage room";
        }
        else if(currentCheckpoint == 6)
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
    public void DisplayEndingText(string ending)
    {
        if(ending == "scaredyCat")
        {
            //"having barely seen anything / startled by a bump in the night, our
            //   protagonist flees with his tail between his legs."
            // -or-
            //"...you flee with your tail between your legs."
            endingUIText.text = "scaredycatending";
        }
        else if(ending == "sensible")
        {
            //"considering the unearthly sounds emanating from the basement, 
            //  our protagonist makes the sensible decision to leave and consider other employment."
            // -or-
            // "...you make the sensible decision..."
            endingUIText.text = "sensibleending";
        }
        else if(ending == "caught")
        {
            
        }

    }
}
