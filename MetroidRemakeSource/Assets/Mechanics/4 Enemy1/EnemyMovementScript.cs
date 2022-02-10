using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 1;

    [SerializeField] private Vector3[] enemyPositions;

    private int index;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            enemyPositions[index],Time.deltaTime * enemySpeed);
        if (transform.position == enemyPositions[index])
        {
            if (index == enemyPositions.Length - 1)
                index = 0;
            else
                index++;
        }
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
