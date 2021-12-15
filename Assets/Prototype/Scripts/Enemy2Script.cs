using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy2Script : MonoBehaviour
{
    #region Inspector
    
    public SpriteRenderer sp;
    public GameObject enemyBullet;
    public GameObject EnregyBall;
    [SerializeField] private int hitsTillDestroy;
    [SerializeField]  private float speed = 1; 
    [SerializeField] private float delay = 1.5f;
    [SerializeField] private float bulletSpeed = 1.5f;
    [SerializeField] private Vector2[] bulletDirections;
    [SerializeField] private float dropRate;
    [SerializeField] private float hitDelay;
    
    #endregion

    #region Fields
    
    [HideInInspector] public Vector3 playerPosition;
    [HideInInspector] public bool attackPlayer;
    private Vector2 playerTarget;
    private bool explode;
    private float timer;
    private int hitsCounter;
    private float delayCounter;

    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameObject.GetComponent<AudioSource>().Play(0);
            hitsCounter++;
            if (hitsCounter == hitsTillDestroy)
            {
                if (Random.Range(0f, 1f) <= dropRate)
                {
                    Instantiate(EnregyBall, transform.position, transform.rotation);
                }
                gameObject.GetComponent<Animator>().SetTrigger("EnemyDead");
                Destroy(gameObject,GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            }
            gameObject.GetComponent<Animator>().SetTrigger("EnemyHit");
            delayCounter = hitDelay;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Platform")&& attackPlayer)
        {
            attackPlayer = false;
            Destroy(gameObject.GetComponentInChildren<CircleCollider2D>());
            explode = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        if (delayCounter > 0)
        {
            delayCounter -= Time.deltaTime;
            return;
        }
        
        if (attackPlayer)
        {
            playerTarget = new Vector2(playerPosition.x, transform.position.y-5f);
            transform.position = Vector2.MoveTowards(transform.position, playerTarget,
                Time.deltaTime * speed); // chase player until the enemy is on the ground
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
