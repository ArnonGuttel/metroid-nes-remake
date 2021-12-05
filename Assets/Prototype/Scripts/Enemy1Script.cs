using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy1Script : MonoBehaviour
{
    public SpriteRenderer sp;
    public GameObject EnregyBall;

    [SerializeField] private float hitDelay;
    [SerializeField] private int hitsTillDestroy;
    [SerializeField] private float dropRate;

    private float delayCounter;
    private int hitCounter;
    private int index;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            hitCounter++;
            if (hitCounter == hitsTillDestroy)
            {
                if (Random.Range(0f, 1f) <= dropRate)
                {
                    Instantiate(EnregyBall, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
            sp.color = Color.red;
            GetComponent<WaypointFollower>().delayCounter = hitDelay;
        }
    }
}