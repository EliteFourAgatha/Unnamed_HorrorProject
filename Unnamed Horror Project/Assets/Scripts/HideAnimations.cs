using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAnimations : MonoBehaviour
{
    public bool isHiding = false;
    public Animator hideAnimator;
    public void HideBehindCouch()
    {
        hideAnimator.Play("HideOnCouch", 0, 0.0f);
    }
    public void UnhideFromCouch()
    {
        hideAnimator.Play("UnhideFromCouch", 0, 0.0f);
    }
}
