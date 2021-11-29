using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    public GameObject EnemiesManager;
    public GameObject StartMessage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            EnemiesManager.SetActive(true);
            StartMessage.SetActive(false);
        }
    }
    
}
