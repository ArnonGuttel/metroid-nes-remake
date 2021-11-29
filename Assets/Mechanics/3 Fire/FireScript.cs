using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    #region Inspector

    public GameObject ReferencedBullet;

    [SerializeField] private float bulletSpeed = 1;

    #endregion

    #region Fields

    private Vector2 currentDirection;

    #endregion


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            print("shoot Right!");
            currentDirection = Vector2.right;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            print("shoot Left!");
            currentDirection = Vector2.left;
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            print("shoot Up!");
            currentDirection = Vector2.up;
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            print("shoot Down!");
            currentDirection = Vector2.down;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(ReferencedBullet);
            bullet.transform.position = transform.position;
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = currentDirection*bulletSpeed;
            Destroy(bullet,1.5f); // Destroy bullet on timer or on collision
        }
    }
}
