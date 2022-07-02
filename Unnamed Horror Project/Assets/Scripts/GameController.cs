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
            objectiveText.text = "Find the tools left for you in the basement";
        }
        else if(currentCheckpoint == 2)
        {
            objectiveText.text = "Repair leak in the bathroom sink";
        }
        else if(currentCheckpoint == 3)
        {
            objectiveText.text = "Fix TV in open storage room";
        }
        //Shouldn't be fuse right away.
        // Lights are on initially.
        // Fuse is blown when player finishes fixing TV
        // -Only lights to main storage area affected
        // --Laundry lights still on / function (on if player left them on)
        // --Same for bathroom light
        // Do shadow scare as planned
        // Instead of ultimatum moment, it's a tension moment.
        // -Realized game lacks tension. Need this moment earlier.
        // -"Crap looks like I blew a fuse. Better go find a replacement"
        // Later on, when final closet moment happens, all lights in basement go out at once.
        // More of a supernatural feel to it.
        //
        // - great idea for better final closet trigger placement
        // -final fix is actually laundry window
        // -player fixes window, goes to leave room, and lights to out. door creaks open.
        // -player scared, most will hide in the lockers you placed there. perfect.

        else if(currentCheckpoint == 4)
        {
            objectiveText.text = "Find extra fuse in back room";
        }
        else if(currentCheckpoint == 5)
        {
            objectiveText.text = "Restore power to the basement";
        }
        else if(currentCheckpoint == 6)
        {
            objectiveText.text = "Fix the laundry room window";
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
