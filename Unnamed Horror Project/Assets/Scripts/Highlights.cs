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
    //public Camera mainCamera;
    Camera mainCamera;
    void Start()
    {
        SetCursorTexture(normalCursorTexture);
        mainCamera = Camera.main;
    }
    void Update()
    {
        HighlightObjectInCenterOfCam();
    }
    void HighlightObject(GameObject gameObject)
    {
        if (lastHighlightedObject != gameObject)
        {
            ClearHighlighted();
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
        float rayDistance = 50f;
        // Ray from the center of the viewport.
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit rayHit;
        // Check if we hit something.
        if (Physics.Raycast(ray, out rayHit, rayDistance))
        {
            // Get the object that was hit.
            GameObject hitObj = rayHit.collider.gameObject;

            if(hitObj.GetComponent<Collider>().gameObject.tag == "Lightswitch")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<Lightswitch>().ExecuteLightSwitch();
                    }
                }
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "MainPaper")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().PickUpMainPaper();
                    }
                }
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "FuseBox")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().ExecuteFuseBoxTrigger();
                    }
                }
            }
            // -- LEVEL 2 ONLY OBJECTS --
            // Burn the plant in right dungeon room
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "BurnPlant")
            {
                    HighlightObject(hitObj);
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        HighlightObject(hitObj);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<Level2Triggers>().ExecuteBurnPlant();
                        }
                    }
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
