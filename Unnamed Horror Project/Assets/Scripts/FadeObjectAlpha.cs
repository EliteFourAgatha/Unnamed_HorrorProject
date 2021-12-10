using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fade object alpha based on proximity.
//  As player gets closer to object, it starts to fade like a mirage
public class FadeObjectAlpha : MonoBehaviour
{
    public GameObject fakeCloset;
    public GameObject player;
    public MeshRenderer[] meshRenderers;
    public MeshRenderer testRenderer;
 
    public float proximity = 10;
    private float absDistance;
    private float alpha;
    private Vector3 playerPosition;

    void Start()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
    }

    void Update ()
    {
        playerPosition = player.transform.position;
        absDistance = Vector3.Distance(playerPosition, fakeCloset.transform.position);
        Debug.Log("abs dist: " + absDistance);
        DetermineAlphaByDistance();
        if(Vector3.Distance(playerPosition, fakeCloset.transform.position) <= 0)
        {
            fakeCloset.SetActive(false);
        }
    }
    public void DetermineAlphaByDistance()
    {
        Color tempColor = testRenderer.material.color;
        //Do 1 - the alpha value to get the inverse, so that it fades out instead of in
        tempColor.a = 1 - (Mathf.InverseLerp (proximity, 0.1f,
        Vector3.Distance (playerPosition, fakeCloset.transform.position)));
        Debug.Log("alpha:" + tempColor.a);
        FadeMultipleObjects(tempColor);
    }
    //Change all mesh renderer object alphas at once
    public void FadeMultipleObjects(Color newColor)
    {
        foreach (MeshRenderer meshRend in meshRenderers)
        {
            meshRend.material.color = newColor;
        }
    }
}
