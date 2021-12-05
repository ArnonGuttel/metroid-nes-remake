using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFireScript : MonoBehaviour
{
    #region Inspector

    public GameObject ReferencedBullet;
    public GameObject Gun;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletTimer;
    
    #endregion

    #region Fields

    [HideInInspector] public bool canFire;
    private Vector2 currentDirection = Vector2.right;
    private Quaternion transformRotation;

    #endregion


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Gun.transform.localPosition = new Vector3(0.5f, 0, 0);
            currentDirection = Vector2.right;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Gun.transform.localPosition = new Vector3(-0.5f, 0, 0);
            currentDirection = Vector2.left;
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Gun.transform.localPosition = new Vector3(0, 0.5f, 0);
            currentDirection = Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {   
            if (!canFire)
                return;
            GameObject bullet = Instantiate(ReferencedBullet);
            bullet.transform.position = transform.position;
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = currentDirection*bulletSpeed;
            Destroy(bullet,bulletTimer); // Destroy bullet on timer or on collision
        }
    }
}