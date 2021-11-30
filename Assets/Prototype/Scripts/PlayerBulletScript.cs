using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;
        if (other.CompareTag("Platform") || other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}