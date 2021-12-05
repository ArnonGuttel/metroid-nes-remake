using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPowerUpScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<PlayerMovement>().rollPowerUp = true;
        Destroy(gameObject);
    }
}
