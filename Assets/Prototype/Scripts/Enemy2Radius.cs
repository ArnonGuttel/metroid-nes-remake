using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Radius : MonoBehaviour
{
    public Enemy2Script script;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            script.playerPosition = other.gameObject.transform.position;
            script.attackPlayer = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
        {
            if (script.attackPlayer)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    script.playerPosition = other.gameObject.transform.position;
                    CircleCollider2D circle = GetComponent<CircleCollider2D>();
                    circle.radius -= 0.1f * Time.deltaTime;
                }
            }
        }
}
