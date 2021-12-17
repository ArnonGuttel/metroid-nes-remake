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
        bool ground = gameObject.GetComponent<PlayerMovement>().onGround;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            Gun.transform.localPosition = new Vector3(0.5f, 0.3f, 0);
            currentDirection = Vector2.right;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            Gun.transform.localPosition = new Vector3(-0.5f, 0.3f, 0);
            currentDirection = Vector2.left;
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                if (currentDirection == Vector2.right)
                    Gun.transform.localPosition = new Vector3(0.1f, 0.5f, 0);
                else if (currentDirection == Vector2.left)
                    Gun.transform.localPosition = new Vector3(-0.1f, 0.5f, 0);
                currentDirection = Vector2.up;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {   
            if (!canFire)
                return;
            GameObject bullet = Instantiate(ReferencedBullet);
            bullet.transform.position = Gun.transform.position;
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = currentDirection*bulletSpeed;
            Destroy(bullet,bulletTimer); // Destroy bullet on timer or on collision
            gameObject.GetComponent<Animator>().SetTrigger("Fire");
        }

        if (currentDirection == Vector2.up && !Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.GetComponent<Animator>().SetBool("IsGunUp", true);
            
            gameObject.GetComponent<Animator>().SetBool("IsGunUpJump", !ground);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("IsGunUp", false);
            gameObject.GetComponent<Animator>().SetBool("IsGunUpJump", false);
        }
            
    }
}