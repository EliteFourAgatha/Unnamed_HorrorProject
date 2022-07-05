using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlights : MonoBehaviour
{
    GameObject lastHighlightedObject;

    [SerializeField] private Image cursorUI;
    [SerializeField] private Sprite normalCursor;
    [SerializeField] private Sprite interactCursor;
    Camera mainCamera;
    [SerializeField] private GameController gameController;
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
    void HighlightObject(GameObject gameObject, bool uiEnabled)
    {
        if (lastHighlightedObject != gameObject)
        {
            ClearHighlighted();

            lastHighlightedObject = gameObject;
            if(uiEnabled)
            {
                cursorUI.sprite = interactCursor;
            }
        }
    } 
    void ClearHighlighted()
    {
        if (lastHighlightedObject != null)
        {
            //lastHighlightedObject.GetComponent<MeshRenderer>().material = originalMat;
            
            lastHighlightedObject = null;
            cursorUI.sprite = normalCursor;
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

            switch(hitObj.GetComponent<Collider>().gameObject.tag)
            {
                case "Lightswitch":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<Lightswitch>().UseLightSwitch();
                        }
                    }
                    else
                    {
                        ClearHighlighted();
                    }
                    break;

                case "LockedDoor":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<Triggers>().TriggerAudioOnly();
                        }
                    }
                    else
                    {
                        ClearHighlighted();
                    }
                    break;

            }

            if(hitObj.GetComponent<Collider>().gameObject.tag == "Lightswitch")
            {
                /*
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj, true);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<Lightswitch>().UseLightSwitch();
                    }
                }
                else
                {
                    ClearHighlighted();
                }
                */
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "LockedDoor")
            {
                /*
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj, true);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<Triggers>().TriggerAudioOnly();
                    }
                }
                else
                {
                    ClearHighlighted();
                }
                */
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "OpenableDoor")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj, true);
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
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "KnockOnDoor")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj, true);
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
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "Snacks")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj, true);
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
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "SinkLeak")
            {
                var radialRef = hitObj.gameObject.GetComponent<RadialProgressBar>();
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj, false);
                    radialRef.canUpdateSink = true;
                    Debug.Log("true");
                }
                else
                {
                    radialRef.canUpdateSink = false;
                    Debug.Log("false");
                    ClearHighlighted();
                }                                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "FixableVendMachine")
            {
                var radialRef = hitObj.gameObject.GetComponent<RadialProgressBar>();
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj, false);
                    radialRef.canUpdateVendMachine = true;
                }
                else
                {
                    radialRef.canUpdateVendMachine = false;
                    ClearHighlighted();
                }                                
            }    
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "LaundryWindow")
            {
                var radialRef = hitObj.gameObject.GetComponent<RadialProgressBar>();
                var triggerRef = hitObj.gameObject.GetComponent<Triggers>();
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 5f)
                {
                    if(triggerRef.canCloseWindow)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            triggerRef.CloseLaundryWindow();
                        }
                    }
                    else
                    {
                        radialRef.canUpdateWindow = true;
                        Debug.Log("window-true");
                    }
                }
                else
                {
                    radialRef.canUpdateWindow = false;
                    Debug.Log("window-false");
                    ClearHighlighted();
                }                                
            }       

            else if(hitObj.GetComponent<Collider>().gameObject.tag == "ToolBox")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    if(gameController.currentCheckpoint == 1 && !gameController.playerHasToolBox)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().PickUpToolBox();
                        }
                    }
                }
                else
                {
                    ClearHighlighted();
                }                
            }
            
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "PickUpBox")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    if(!gameController.playerHasPickUpBox)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().PickUpCardboardBox();
                        }
                    }
                }
                else
                {
                    ClearHighlighted();
                }                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "CoffeeTable")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    if(gameController.playerHasPickUpBox)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().InteractWithCoffeeTable();
                        }
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
                    HighlightObject(hitObj, true);
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
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "Fuse")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    if(gameController.currentCheckpoint == 4)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().PickUpFuse();
                        }
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
                    if(gameController.currentCheckpoint == 5 && gameController.playerHasFuse)
                    {
                        HighlightObject(hitObj, true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            hitObj.GetComponent<Collider>().gameObject.GetComponent<MainTriggers>().InteractWithFuseBox();
                        }
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
                    HighlightObject(hitObj, true);
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
                    HighlightObject(hitObj, true);
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
                    HighlightObject(hitObj, true);
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
                    HighlightObject(hitObj, true);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hitObj.GetComponent<Collider>().gameObject.GetComponent<Triggers>().InteractWithExpositionNote();
                    }
                }
                else
                {
                    ClearHighlighted();
                }                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "EscapeLadder")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 5f)
                {
                    HighlightObject(hitObj, true);
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
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "MainPaper")
            {
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj, true);
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
            else
            {
                if(hitObj.GetComponent<Collider>().gameObject.tag == "SinkLeak")
                {
                    var radialRef = hitObj.gameObject.GetComponent<RadialProgressBar>();
                    radialRef.canUpdateSink = false;
                    Debug.Log("adsfasdf");
                }
                ClearHighlighted();
            }
        }
        else
        {
            /*
            if(hitObj.GetComponent<Collider>().gameObject.tag == "SinkLeak")
            {
                var radialRef = hitObj.gameObject.GetComponent<RadialProgressBar>();
                radialRef.canUpdateSink = false;
            }
            */
            ClearHighlighted();
        }
    }
}
