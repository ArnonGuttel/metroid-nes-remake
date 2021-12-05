using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyBallScript : MonoBehaviour
{
    [SerializeField] private float EnergyBallTimer;
    private void Start()
    {
        Destroy(gameObject,EnergyBallTimer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().addEnergy();
            Destroy(gameObject);
        }
    }
}
