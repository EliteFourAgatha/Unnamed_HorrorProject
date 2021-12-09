using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectAlpha : MonoBehaviour
{
    public GameObject fakeCloset;
    public GameObject player;
    public MeshRenderer[] meshRenderers;
    public MeshRenderer testRenderer;
 
    private float proximity = 10;
    private float absDistance;
    private float alpha;
    private Vector3 playerPosition;

    void Awake()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
    }

    void Update ()
    {
        playerPosition = player.transform.position;
        absDistance = Vector3.Distance(playerPosition, fakeCloset.transform.position);
        Debug.Log("abs dist: " + absDistance);
        DetermineAlphaByDistance();
    }
    public void DetermineAlphaByDistance()
    {
        Color tempColor = testRenderer.material.color;
        //Do 1 - the alpha value to get the inverse, so that it fades out instead of in
        tempColor.a = 1 - (Mathf.InverseLerp (proximity, 0.1f,
        Vector3.Distance (playerPosition, fakeCloset.transform.position)));
        Debug.Log("alpha:" + tempColor.a);
        //testRenderer.material.color = tempColor;
        FadeMultipleObjects(tempColor);
    }
    //Change all closet mesh renderer alphas at once
    public void FadeMultipleObjects(Color newColor)
    {
        foreach (MeshRenderer meshRend in meshRenderers)
        {
            meshRend.material.color = newColor;
            Debug.Log("fading " + meshRend);
        }
    }
}
