using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private BoxCollider2D playerFeetCollision2D;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private float ShortJumpDecrease = 5.0f;
    [HideInInspector] public float knockBackCount;
    [HideInInspector] public bool knockFromRight;
    private bool ground;
    public float knockBackLength;
    public float knockBack;
    public bool rollPowerUp;
    private bool rolling;

    void Update()
    {
        if (knockBackCount <= 0)
        {
            float dirx = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);
            if (Input.GetKey(KeyCode.UpArrow))
                rolling = false;
            if (Input.GetKeyDown(KeyCode.Space) && ground) // long jump
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                rolling = false;
            }
            else if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0.0f) // short jump
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - ShortJumpDecrease);

            if (Input.GetKey(KeyCode.DownArrow) && rollPowerUp && ground)
                changeSize();
        }

        else
        {
            if (knockFromRight)
                rb.velocity = new Vector2(-knockBack, knockBack);
            if (!knockFromRight)
                rb.velocity = new Vector2(knockBack, knockBack);
            knockBackCount -= Time.deltaTime;
        }

        if (!rolling)
        {
            transform.localScale = new Vector3(1,1,1);
            gameObject.GetComponent<PlayerFireScript>().canFire = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            ground = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            ground = false;
    }
    
    private void changeSize()
    {
        if (!rolling)
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(scale.x / 2, scale.y / 2, scale.z);
            rolling = true;
            gameObject.GetComponent<PlayerFireScript>().canFire = false;
        }
    }
}