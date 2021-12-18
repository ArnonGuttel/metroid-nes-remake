using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy2Script : MonoBehaviour
{
    #region Inspector

    public GameObject enemyBullet;
    public GameObject enregyBall;
    [SerializeField] private int hitsTillDestroy;
    [SerializeField] private float enemySpeed; // 
    [SerializeField] private float timeTillExplosion;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletTime;
    [SerializeField] private int bulletsNumber;
    [SerializeField] private Vector2[] bulletDirections;
    [SerializeField] private float dropRate;
    [SerializeField] private float hitDelay;

    #endregion

    #region Fields

    [HideInInspector] public Vector3 playerPosition;
    [HideInInspector] public bool attackPlayer;
    private Vector3 _initPosition;
    private Vector2 _playerTarget;
    private bool _explode;
    private float _timer;
    private int _hitsCounter;
    private float _delayCounter;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _initPosition = gameObject.transform.position;
    }

    private void OnEnable()
        // Reset all fields
    {
        transform.position = _initPosition;
        _explode = false;
        attackPlayer = false;
        _hitsCounter = 0;
        _delayCounter = 0;
        _timer = 0;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameObject.GetComponent<AudioSource>().Play(0);
            _hitsCounter++;
            if (_hitsCounter == hitsTillDestroy)
                EnemyDead();
            gameObject.GetComponent<Animator>().SetTrigger("EnemyHit");
            _delayCounter = hitDelay;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") && attackPlayer)
            // When the Enemy touch the ground it will start a timer till explosion
        {
            attackPlayer = false;
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
            _explode = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        if (_delayCounter > 0)
        {
            _delayCounter -= Time.deltaTime;
            return;
        }

        if (attackPlayer)
            // chase player until the enemy is on the ground
        {
            _playerTarget = new Vector2(playerPosition.x, transform.position.y - 5f);
            transform.position = Vector2.MoveTowards(transform.position, _playerTarget,
                Time.deltaTime * enemySpeed);
        }

        if (_explode)
            // if the timer reached to timeTillExplosion, explode and instantiate enemy bullets
        {
            if (_timer >= timeTillExplosion)
            {
                InstantiateBullets();
                deactiveEnemy();
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }
    }

    #endregion

    #region Methods

    private void deactiveEnemy()
    {
        GameManager.addToDeadEnemies(gameObject);
        gameObject.SetActive(false);
    }

    private void EnemyDead()
        // check for energy ball instantiation, play "EnemyDead" animation, and update GameManager DeadEnemies. 
    {
        if (Random.Range(0f, 1f) <= dropRate)
        {
            Instantiate(enregyBall, transform.position, transform.rotation);
        }

        gameObject.GetComponent<Animator>().SetTrigger("EnemyDead");
        gameObject.GetComponent<Collider2D>().enabled = false;
        Invoke("deactiveEnemy", GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void InstantiateBullets()
    {
        for (int i = 0; i < bulletsNumber; i++)
        {
            GameObject bullet = Instantiate(enemyBullet);
            Vector3 temp = transform.position;
            temp.y += 0.3f; 
            bullet.transform.position = temp;
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = bulletDirections[i] * bulletSpeed;
            Destroy(bullet, bulletTime);
        }
    }

    #endregion
}