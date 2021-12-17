using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy1Script : MonoBehaviour
{
    public GameObject EnregyBall;

    [SerializeField] private float hitDelay;
    [SerializeField] private int hitsTillDestroy;
    [SerializeField] private float dropRate;
    private Vector3 _initPosition;
    private float delayCounter;
    private int hitCounter;

    private void Awake()
    {
        _initPosition = gameObject.transform.position;
    }

    private void OnEnable()
    {
        transform.position = _initPosition;
        hitCounter = 0;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameObject.GetComponent<AudioSource>().Play(0);
            hitCounter++;
            if (hitCounter == hitsTillDestroy)
            {
                if (Random.Range(0f, 1f) <= dropRate)
                {
                    Instantiate(EnregyBall, transform.position, transform.rotation);
                }
                gameObject.GetComponent<Animator>().SetTrigger("EnemyDead");
                gameObject.GetComponent<Collider2D>().enabled = false;
                Invoke("deactiveEnemy",GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            }
            gameObject.GetComponent<Animator>().SetTrigger("EnemyHit");
            GetComponent<WaypointFollower>().delayCounter = hitDelay;
        }
    }

    private void deactiveEnemy()
    {
        GameManager.addToDeadEnemies(gameObject);
        gameObject.SetActive(false);
    }
}