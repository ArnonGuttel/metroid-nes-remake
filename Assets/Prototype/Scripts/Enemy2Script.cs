using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy2Script : MonoBehaviour
{
    #region Inspector
    
    public SpriteRenderer sp;
    public GameObject enemyBullet;
    public float platformGround = -2.5f;
    [SerializeField] private int hitsTillDestroy;
    [SerializeField]  private float speed = 1; 
    [SerializeField] private float delay = 1.5f;
    [SerializeField] private float bulletSpeed = 1.5f;
    [SerializeField] private Vector2[] bulletDirections;

    #endregion

    #region Fields
    
    [HideInInspector] public Vector3 playerPosition;
    [HideInInspector] public bool attackPlayer;
    private Vector2 playerTarget;
    private bool explode;
    private float timer;
    private int hitsCounter;

    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            hitsCounter++;
            if (hitsCounter == hitsTillDestroy)
                Destroy(gameObject);
            sp.color = Color.red;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("You got hit!");
            sp.color = Color.yellow;
        }
    }

    private void Update()
    {
        if (attackPlayer)
        {
            sp.color = Color.cyan;
            playerTarget = new Vector2(playerPosition.x, platformGround-1f);
            transform.position = Vector2.MoveTowards(transform.position, playerTarget,
                Time.deltaTime * speed); // chase player until the enemy is on the ground
            if (transform.position.y <= platformGround)
            {
                Destroy(GetComponent(typeof(CircleCollider2D)));
                attackPlayer = false;
                explode = true;
                sp.color = Color.red;
            }
        }
        if (explode)
        {
            if (timer >= delay)
            {
                for (int i = 0; i < 4; i++)
                {
                    GameObject bullet = Instantiate(enemyBullet);
                    Vector3 temp = transform.position;
                    temp.y += 0.3f;
                    bullet.transform.position = temp;
                    Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                    bulletRb.velocity = bulletDirections[i] * bulletSpeed;
                    Destroy(bullet, 1.5f);
                }
                Destroy(gameObject);
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
