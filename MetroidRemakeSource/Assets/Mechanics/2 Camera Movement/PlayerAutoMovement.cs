using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    private int direction = 1;
    // Start is called before the first frame update


    private void Update()
    {
        if (transform.position.x >= 8)
            direction = -1;
        if (transform.position.x <= -8)
            direction = 1;
        rb.velocity = new Vector2(direction * 3, 0);
    }
}
