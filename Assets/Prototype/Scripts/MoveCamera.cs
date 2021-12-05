using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject Camera;

    private int counter;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (counter == 0)
            {
                Camera.GetComponent<CameraScript>().MoveCamera(true);
                counter++;
            }
            else
            {
                Camera.GetComponent<CameraScript>().MoveCamera(false);
                counter--;
            }
            
        }
        
    }
}
