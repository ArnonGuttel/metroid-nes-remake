using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Rigidbody2D playerRB;

    private void Update()
    {
        Vector3 currentCameraPosition = transform.position;
        currentCameraPosition.x = playerRB.transform.position.x;
        transform.position = currentCameraPosition;
    }
}
