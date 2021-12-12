using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy1Script : MonoBehaviour
{
    public GameObject EnregyBall;

    [SerializeField] private float hitDelay;
    [SerializeField] private int hitsTillDestroy;
    [SerializeField] private float dropRate;

    private float delayCounter;
    private int hitCounter;
    private int index;
    private Vector3 _originalPosition;

    //
    // private void OnBecameVisible()
    // {
    //     gameObject.SetActive(true);
    //     print("pinuk");
    //     resetEnemy();
    // }
    //
    // private void OnBecameInvisible()
    // {
    //     print("notPinuk");
    // }
    //
    // private void Start()
    // {
    //     _originalPosition= transform.position;
    // }
    //
    // private void resetEnemy()
    // {
    //     transform.position = _originalPosition;
    //     delayCounter = 0;
    //     hitCounter = 0;
    //     index = 0;
    // }
    
    
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
                gameObject.GetComponent<Animator>().SetTrigger("EnemyDead");
                Destroy(gameObject,GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            }
            gameObject.GetComponent<Animator>().SetTrigger("EnemyHit");
            GetComponent<WaypointFollower>().delayCounter = hitDelay;
        }
    }
}