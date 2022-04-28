using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonAnimations : MonoBehaviour
{
    public bool isHiding = false;
    public Animator playerAnimator;
    public void HideBehindCouch()
    {
        playerAnimator.Play("HideOnCouch", 0, 0.0f);
    }
    public void UnhideFromCouch()
    {
        playerAnimator.Play("UnhideFromCouch", 0, 0.0f);
    }
    public IEnumerator DropFromEggAndGetUp()
    {
        playerAnimator.Play("DropFromEgg", 0, 0.0f);
        yield return new WaitForSeconds(1f);
        playerAnimator.Play("GetUpFromFall", 0, 0.0f);
    }
}
