using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private bool canJump;

    public Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector2 StartPosition;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private float ShortJumpDecrease = 5.0f;

// Update is called once per frame
    void Update()
    {
        float dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && canJump) // long jump
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        else if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0.0f) // short jump
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - ShortJumpDecrease);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            canJump = true;
        else if (other.gameObject.CompareTag("Edge")) // if fallen from game edge
            transform.position = StartPosition;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            canJump = false;
    }
}