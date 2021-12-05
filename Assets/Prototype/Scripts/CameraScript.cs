using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Rigidbody2D playerRB;
    [SerializeField] private GameObject cameraMinX;
    [SerializeField] private GameObject cameraMaxX;
    [SerializeField] private float cameraSize;
    [SerializeField] private Vector3[] cameraPositions;
    
    private bool _cameraLocked;
    private int cameraPositionIndex = 0;

    private void Update()
    {
        Vector3 currentCameraPosition = transform.position;
        Vector3 currentPlayerPosition = playerRB.transform.position;
        if (currentPlayerPosition.x + cameraSize >= cameraMaxX.transform.position.x
            || currentPlayerPosition.x - cameraSize <= cameraMinX.transform.position.x)
            _cameraLocked = true;
        else
            _cameraLocked = false;

        if (!_cameraLocked)
        {
            currentCameraPosition.x = currentPlayerPosition.x;
            transform.position = currentCameraPosition;
        }
    }

    public void MoveCamera(bool goRight)
    {
        if (goRight)
            cameraPositionIndex++;
        else
            cameraPositionIndex--;
        transform.position = cameraPositions[cameraPositionIndex];
    }
}
