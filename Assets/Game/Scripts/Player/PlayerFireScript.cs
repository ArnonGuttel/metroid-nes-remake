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

    #endregion

    #region MonoBehaviour

    void Update()
    {
        bool ground = gameObject.GetComponent<PlayerMovement>().onGround;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveGunRight();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveGunLeft();

        if (Input.GetKey(KeyCode.UpArrow))
            MoveGunUp();

        if (Input.GetKeyDown(KeyCode.S))
            InstantiateBullet();

        // set the current animation according to player current position
        if (currentDirection == Vector2.up && !Input.GetKeyDown(KeyCode.LeftArrow) &&
            !Input.GetKeyDown(KeyCode.RightArrow))
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

    #endregion

    #region Methods

    private void MoveGunRight()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        Gun.transform.localPosition = new Vector3(0.5f, 0.3f, 0); // change gun position according to Samus sprite.
        currentDirection = Vector2.right;
    }

    private void MoveGunLeft()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        Gun.transform.localPosition = new Vector3(-0.5f, 0.3f, 0); // change gun position according to Samus sprite.
        currentDirection = Vector2.left;
    }

    private void MoveGunUp()
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

    private void InstantiateBullet()
    {
        if (!canFire)
            return;
        GameObject bullet = Instantiate(ReferencedBullet);
        bullet.transform.position = Gun.transform.position;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = currentDirection * bulletSpeed;
        Destroy(bullet, bulletTimer); // Destroy bullet on timer or on collision
        gameObject.GetComponent<Animator>().SetTrigger("Fire");
    }

    #endregion
}