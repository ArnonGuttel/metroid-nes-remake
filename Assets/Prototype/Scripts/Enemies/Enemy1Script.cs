using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy1Script : MonoBehaviour
{
    #region Inspector
    
    public GameObject EnregyBall;
    [SerializeField] private float hitDelay; // for how long to freeze enemy movement after bullet hit
    [SerializeField] private int hitsTillDestroy;
    [SerializeField] private float dropRate; // drop rate for energy ball

    #endregion

    #region Fields

    private Vector3 _initPosition;
    private int _hitCounter;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _initPosition = gameObject.transform.position;
    }

    private void OnEnable()
    {
        transform.position = _initPosition;
        _hitCounter = 0;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameObject.GetComponent<AudioSource>().Play(0);
            _hitCounter++;
            if (_hitCounter == hitsTillDestroy)
                EnemyDead();
            gameObject.GetComponent<Animator>().SetTrigger("EnemyHit");
            GetComponent<WaypointFollower>().delayCounter = hitDelay;
        }
    }
    
    #endregion

    #region Methods

    private void EnemyDead()
    // check for energy ball instantiation, play "EnemyDead" animation, and update GameManager DeadEnemies. 
    {
        if (Random.Range(0f, 1f) <= dropRate)
        {
            var transform1 = transform;
            Instantiate(EnregyBall, transform1.position, transform1.rotation);
        }
        gameObject.GetComponent<Animator>().SetTrigger("EnemyDead");
        gameObject.GetComponent<Collider2D>().enabled = false;
        Invoke(nameof(DeActiveEnemy),GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
    
    private void DeActiveEnemy()
    {
        GameManager.addToDeadEnemies(gameObject);
        gameObject.SetActive(false);
    }

    #endregion
    



}