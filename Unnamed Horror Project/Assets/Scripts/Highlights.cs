using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlights : MonoBehaviour
{
    public Material highlightMat;
    Material originalMat;
    GameObject lastHighlightedObject;
    public Texture2D handCursorTexture;
    public Texture2D doorCursorTexture;
    Texture2D normalCursorTexture;
    
    void Start()
    {
        SetCursorTexture(normalCursorTexture);
    }
    void Update()
    {
        HighlightObjectInCenterOfCam();
    }
    void HighlightObject(GameObject gameObject)
    {
        if (lastHighlightedObject != gameObject)
        {
            //ClearHighlighted();
            //originalMat = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            //gameObject.GetComponent<MeshRenderer>().sharedMaterial = highlightMat;
            gameObject.GetComponent<Outline>().enabled = true;
            lastHighlightedObject = gameObject;
            SetCursorTexture(doorCursorTexture);
            Cursor.visible = true;
        }
    } 
    void ClearHighlighted()
    {
        if (lastHighlightedObject != null)
        {
            //lastHighlightedObject.GetComponent<MeshRenderer>().sharedMaterial = originalMat;
            lastHighlightedObject.GetComponent<Outline>().enabled = false;
            lastHighlightedObject = null;
            SetCursorTexture(normalCursorTexture);
            Cursor.visible = false;
        }
    } 
    void HighlightObjectInCenterOfCam()
    {
        float rayDistance = 5f;
        // Ray from the center of the viewport.
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit rayHit;
        // Check if we hit something.
        if (Physics.Raycast(ray, out rayHit, rayDistance))
        {
            // Get the object that was hit.
            GameObject hitObject = rayHit.collider.gameObject;
            if(hitObject.tag == "Highlightable")
            {
                HighlightObject(hitObject);
                Debug.Log("Hit: " + hitObject);
            }
            else
            {
                ClearHighlighted();
            }
        }
        else
        {
            ClearHighlighted();
        }
    }

    void SetCursorTexture(Texture2D tex)
    {
        Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
    }
}
