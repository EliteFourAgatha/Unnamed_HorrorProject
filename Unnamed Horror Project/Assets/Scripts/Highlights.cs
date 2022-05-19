using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlights : MonoBehaviour
{
    GameObject lastHighlightedObject;

    public Material highlightMaterialTEST;
    Material originalMat;

    public Image UIInteractImage;
    Camera mainCamera;
    public GameController gameController;
    private string noKeyFoundString = "It's locked but I see a key hole...";
    //private string doorLockedString = "Locked";
    void Start()
    {
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

            //originalMat = gameObject.GetComponent<MeshRenderer>().material;
            //gameObject.GetComponent<MeshRenderer>().material = highlightMaterialTEST;



            lastHighlightedObject = gameObject;
            UIInteractImage.enabled = true;
        }
    } 
    void ClearHighlighted()
    {
        if (lastHighlightedObject != null)
        {
            //lastHighlightedObject.GetComponent<MeshRenderer>().material = originalMat;
            
            lastHighlightedObject = null;
            UIInteractImage.enabled = false;
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
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<Lightswitch>().UseLightSwitch();
                    }
                }
                else
                {
                    ClearHighlighted();
                }
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "LockedDoor")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<Triggers>().TriggerAudioOnly();
                    }
                }
                else
                {
                    ClearHighlighted();
                }
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "OpenableDoor")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        var doorScript = hitObj.GetComponent<Collider>().gameObject.GetComponent<DoorController>();
                        var doorClosed = doorScript.doorClosed;
                        if(doorClosed)
                        {
                            doorScript.OpenDoor();
                        }
                        else
                        {
                            doorScript.CloseDoor();
                        }
                    }
                }
                else
                {
                    ClearHighlighted();
                }
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "Snacks")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<Triggers>().TriggerAudioOnly();
                    }
                }
                else
                {
                    ClearHighlighted();
                }                                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "LaundryWindow")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 4f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<Triggers>().CloseLaundryWindow();
                    }
                }
                else
                {
                    ClearHighlighted();
                }                                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "Couch")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 2f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<Triggers>().InteractWithCouch();  
                    }
                }
                else
                {
                    ClearHighlighted();
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
                else
                {
                    ClearHighlighted();
                }                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "Pliers")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().PickUpPliers();
                    }
                }
                else
                {
                    ClearHighlighted();
                }                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "MainCar")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<CarInteractions>().AttemptToUseCar();
                    }
                }
                else
                {
                    ClearHighlighted();
                }                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "FuseBox")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().TriggerFuseBox();
                    }
                }
                else
                {
                    ClearHighlighted();
                }                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "Locker")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<Triggers>().InteractWithLocker();
                    }
                }
                else
                {
                    ClearHighlighted();
                }                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "LockedDrawer")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        if(!gameController.playerNeedsKey)
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<Triggers>().InteractWithDrawer();
                        }
                        else
                        {
                            StartCoroutine(gameController.ShowPopupMessage(noKeyFoundString, 2));
                        }
                    }
                }
                else
                {
                    ClearHighlighted();
                }                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "HiddenKey")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().TriggerFoundKey();
                    }
                }
                else
                {
                    ClearHighlighted();
                }                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "ExpositionNote")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().ExecuteExpositionNote();
                    }
                }
                else
                {
                    ClearHighlighted();
                }                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "EscapeLadder")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 7f)
                {
                    HighlightObject(hitObj);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().TriggerEscape();
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
        else
        {
            ClearHighlighted();
        }
    }
}
