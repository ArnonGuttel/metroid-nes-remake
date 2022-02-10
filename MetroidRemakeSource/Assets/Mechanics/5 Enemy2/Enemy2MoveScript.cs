using UnityEngine;

public class Enemy2MoveScript : MonoBehaviour
{
    #region Inspector
    
    public SpriteRenderer sp;
    public GameObject enemyBullet;
    [SerializeField]  private float speed = 1;
    [SerializeField] private float platformGround = -2.5f;
    [SerializeField] private float delay = 1.5f;
    [SerializeField] private float bulletSpeed = 1.5f;
    [SerializeField] private Vector2[] bulletDirections;

    #endregion

    #region Fields

    private Vector3 playerPosition;
    private Vector2 PlayerTarget;
    private bool attackPlayer;
    private bool explode;
    private float timer;

    #endregion
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {       
            playerPosition = other.gameObject.transform.position;
            PlayerTarget = new Vector2(playerPosition.x,platformGround);
            attackPlayer = true;
            sp.color = Color.cyan;}
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {        
            playerPosition = other.gameObject.transform.position;
            PlayerTarget = new Vector2(playerPosition.x,platformGround);
            CircleCollider2D circle = GetComponent<CircleCollider2D>();
            circle.radius -= 0.1f;
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
            transform.position = Vector2.MoveTowards(transform.position, PlayerTarget,
                Time.deltaTime * speed); // chase player until the enemy is on the ground
            if (transform.position.y == platformGround)
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
