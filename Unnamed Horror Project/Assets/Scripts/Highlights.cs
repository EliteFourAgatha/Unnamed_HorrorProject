using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlights : MonoBehaviour
{
    public Material highlightMaterial;
    Material originalMaterial;
    GameObject lastHighlightedObject;
    void HighlightObject(GameObject gameObject){
        if (lastHighlightedObject != gameObject){
            ClearHighlighted();
            originalMaterial = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = highlightMaterial;
            lastHighlightedObject = gameObject;
        }
    } 
    void ClearHighlighted(){
        if (lastHighlightedObject != null){
            lastHighlightedObject.GetComponent<MeshRenderer>().sharedMaterial = originalMaterial;
            lastHighlightedObject = null;
        }
    } 
    void HighlightObjectInCenterOfCam(){
        float rayDistance = 1000.0f;
        // Ray from the center of the viewport.
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit rayHit;
        // Check if we hit something.
        if (Physics.Raycast(ray, out rayHit, rayDistance)){
            // Get the object that was hit.
            GameObject hitObject = rayHit.collider.gameObject;
            if(hitObject.tag == "Highlightable"){
                HighlightObject(hitObject);
            }
            else{
                ClearHighlighted();
            }
        } 
        else{
            ClearHighlighted();
        }
    }
 
    void Update(){
        HighlightObjectInCenterOfCam();
    }
}
