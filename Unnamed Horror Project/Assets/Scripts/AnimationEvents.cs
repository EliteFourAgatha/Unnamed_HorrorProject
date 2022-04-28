using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject eggPodSac;
    public AudioClip squishSFX;
    public GameObject squishLocation;
    Transform playerTF;
    public MouseLook mouseLook;
    void Start()
    {
        playerTF = gameObject.GetComponent<Transform>();
    }
    public void DisableGhostAfterAnimation()
    {
        gameObject.SetActive(false);
    }
    public void DisablePodWhileFalling()
    {
        AudioSource.PlayClipAtPoint(squishSFX, squishLocation.transform.position, 0.5f);
        eggPodSac.SetActive(false);
    }
    public void ResetPlayerRotation()
    {
        //Directly set rotation with eulerAngles
        playerTF.rotation = Quaternion.Euler(0, 180f, 0);
        playerTF.position = new Vector3(0, 0, 0);
        Debug.Log("rotated");
    }
    public void EnableMouse()
    {
        mouseLook.canUseMouse = true;
    }
    public void DisableMouse()
    {
        mouseLook.canUseMouse = false;
    }
}
