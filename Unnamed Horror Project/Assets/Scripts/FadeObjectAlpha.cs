using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fade object alpha based on proximity.
//  As player gets closer to object, it starts to fade like a mirage
public class FadeObjectAlpha : MonoBehaviour
{
    public GameObject fakeCloset;
    public GameObject topOfStairsDoor;
    public GameObject player;
    public MeshRenderer[] meshRenderers;
    public MeshRenderer testRenderer;
    private FadeObjectAlpha fadeObjectAlpha;

    public Material doorTransparentMat;
 
    public float closetProximity = 15;
    public float doorProximity = 15;
    private float absDistance;
    private float alpha;
    private Vector3 playerPosition;
    public enum ObjectType {Closet, TopOfStairsDoor}
    public ObjectType objectType;

    void Start()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        fadeObjectAlpha = gameObject.GetComponent<FadeObjectAlpha>();
    }

    void Update ()
    {
        playerPosition = player.transform.position;
        if(objectType == ObjectType.Closet)
        {
            if(Vector3.Distance(playerPosition, fakeCloset.transform.position) <= 1)
            {
                fakeCloset.SetActive(false);
            }
            FadeOutAlphaByDistance();
        }
        else if(objectType == ObjectType.TopOfStairsDoor)
        {
            if(Vector3.Distance(playerPosition, topOfStairsDoor.transform.position) <= 2)
            {
                fadeObjectAlpha.enabled = false;
            }
            FadeInAlphaByDistance();
        }

    }
    public void FadeOutAlphaByDistance()
    {
        Color tempColor = testRenderer.material.color;        
        tempColor.a = 1 - (Mathf.InverseLerp (closetProximity, 0.1f,
        Vector3.Distance (playerPosition, fakeCloset.transform.position)));
        FadeMultipleObjects(tempColor);
    }
    public void FadeInAlphaByDistance()
    {
        Color tempColor = testRenderer.material.color;
        float distance = Vector3.Distance(topOfStairsDoor.transform.position, playerPosition);
        tempColor.a = doorProximity / distance;
        FadeMultipleObjects(tempColor);

        //doorTransparentMat.color = tempColor;
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
