using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlights : MonoBehaviour
{
    GameObject lastHighlightedObject;

    [SerializeField] private Image cursorUI;
    [SerializeField] private AudioSource toolAudioSource;
    [SerializeField] private GameObject toolAudioObj;
    [SerializeField] private Sprite normalCursor;
    [SerializeField] private Sprite interactCursor;
    [SerializeField] private Image sinkProgressImage;
    [SerializeField] private Image windowProgressImage;
    [SerializeField] private Image vendMachineProgressImage;
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
                case "OpenableDoor":
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
                    else
                    {
                        ClearHighlighted();
                    }
                    break;
                case "KnockOnDoor":
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
                case "Snacks":
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
                case "ToolBox":
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
                    break;
                case "PickUpBox":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        if(gameController.currentCheckpoint == 4)
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
                    }
                    else
                    {
                        ClearHighlighted();
                    }
                    break;
                case "CoffeeTable":
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
                    break;
                case "MainCar":
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
                    break;
                case "Fuse":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        if(gameController.currentCheckpoint == 6)
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
                    break;
                case "FuseBox":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                    {
                        if(gameController.currentCheckpoint == 7 && gameController.playerHasFuse)
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
                    break;
                case "Locker":
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
                    break;
                case "MainPaper":
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
                    break;
                case "EscapeLadder":
                    if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 6f)
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
                    break;
                case "ExpositionNote":
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
                    break;
                case "HiddenKey":
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
                    break;
                case "LockedDrawer":
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
                    break;
                case "LaundryWindow":
                    if(gameController.currentCheckpoint == 8)
                    {
                        var windowProgress = hitObj.gameObject.GetComponent<ProgressBar>();
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
                                HighlightObject(hitObj, true);
                                if(Input.GetKey(KeyCode.E))
                                {
                                    if(!toolAudioSource.isPlaying)
                                    {
                                        toolAudioSource.Play();
                                    }
                                    windowProgressImage.enabled = true;
                                    windowProgress.UpdateWindowFill();
                                }
                            }
                        }
                        else
                        {
                            toolAudioSource.Pause();
                            windowProgressImage.enabled = false;
                            ClearHighlighted();
                        }
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "SinkLeak":
                    if(gameController.currentCheckpoint == 2)
                    {
                        toolAudioObj.SetActive(true);
                        var sinkProgress = hitObj.gameObject.GetComponent<ProgressBar>();
                        if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                        {
                            HighlightObject(hitObj, true);
                            if(Input.GetKey(KeyCode.E))
                            {
                                if(!toolAudioSource.isPlaying)
                                {
                                    toolAudioSource.Play();
                                }
                                sinkProgressImage.enabled = true;
                                sinkProgress.UpdateSinkFill();
                            }
                        }
                        else
                        {
                            toolAudioSource.Pause();
                            sinkProgressImage.enabled = false;
                            ClearHighlighted();
                        }
                        break;                        
                    }
                    else
                    {
                        toolAudioObj.SetActive(false);
                        break;
                    }
                case "FixableVendMachine":
                    if(gameController.currentCheckpoint == 3)
                    {
                        var vendMachineProgress = hitObj.gameObject.GetComponent<ProgressBar>();
                        if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 4f)
                        {
                            HighlightObject(hitObj, true);
                            if(Input.GetKey(KeyCode.E))
                            {
                                if(!toolAudioSource.isPlaying)
                                {
                                    toolAudioSource.Play();
                                }
                                vendMachineProgressImage.enabled = true;
                                vendMachineProgress.UpdateVendMachineFill();
                            }
                        }
                        else
                        {
                            toolAudioSource.Pause();
                            vendMachineProgressImage.enabled = true;
                            ClearHighlighted();
                        }
                        break;
                    }
                    else
                    {
                        break;
                    }

                //Default runs if no values matched, aka "Else"
                default:
                    sinkProgressImage.enabled = false;
                    if(toolAudioObj.activeInHierarchy)
                    {
                        toolAudioSource.Pause();
                    }
                    ClearHighlighted();
                    break;
            }
        }
    }
        /*

            if(hitObj.GetComponent<Collider>().gameObject.tag == "Lightswitch")
            {
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
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "LockedDoor")
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
                var progressRef = hitObj.gameObject.GetComponent<ProgressBar>();
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj, false);
                    progressRef.canUpdateSink = true;
                    Debug.Log("true");
                }
                else
                {
                    progressRef.canUpdateSink = false;
                    Debug.Log("false");
                    ClearHighlighted();
                }                                
            }
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "FixableVendMachine")
            {
                var progressRef = hitObj.gameObject.GetComponent<ProgressBar>();
                if(Vector3.Distance(gameObject.transform.position, hitObj.transform.position) < 3f)
                {
                    HighlightObject(hitObj, false);
                    progressRef.canUpdateVendMachine = true;
                }
                else
                {
                    progressRef.canUpdateVendMachine = false;
                    ClearHighlighted();
                }                                
            }    
            else if(hitObj.GetComponent<Collider>().gameObject.tag == "LaundryWindow")
            {
                var progressRef = hitObj.gameObject.GetComponent<ProgressBar>();
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
                        progressRef.canUpdateWindow = true;
                        Debug.Log("window-true");
                    }
                }
                else
                {
                    progressRef.canUpdateWindow = false;
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
                    if(gameController.currentCheckpoint == 4)
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
        }
        else
        {
            ClearHighlighted();
        }
        */
}
