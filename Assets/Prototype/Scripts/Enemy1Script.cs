using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    public SpriteRenderer sp;
    [SerializeField] private float enemySpeed = 1;
    [SerializeField] private int hitsTillDestroy;
    [SerializeField] private Vector3[] enemyPositions;

    private int hitCounter;
    private int index;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            enemyPositions[index], Time.deltaTime * enemySpeed);
        if (transform.position == enemyPositions[index])
        {
            if (index == enemyPositions.Length - 1)
                index = 0;
            else
                index++;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            hitCounter++;
            if (hitCounter == hitsTillDestroy)
                Destroy(gameObject);
            sp.color = Color.red;
        }
    }
}