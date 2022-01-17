using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    public Transform playerTF;
    void Start()
    {
        
    }

    void Update()
    {
        RotateToLookAtPlayer();
    }
    void RotateToLookAtPlayer()
    {
        transform.LookAt(playerTF);
        transform.Rotate(0, 90, 0);
    }
}
