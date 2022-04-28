using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    public bool canUseMouse = true;
    void Update()
    {
        if(canUseMouse)
        {
            LookWithMouse();
        }

    }
    private void LookWithMouse()
    {
        //Mouse Input on X and Y axes, * mouse sensitivity, * delta time so that
        // you will rotate at the same speed regardless of frame rate (deltaTime = 
        // time between frames)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        //Clamps camera rotation so you can't look past a certain point 
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
