using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Constants
    
    private const int InitialEnergy = 30;

    #endregion

    #region Inspector

    [SerializeField] private int playerHitDamage;
    [SerializeField] private int energyRestore;
    [SerializeField] private TextMeshProUGUI energyFrame;
    [SerializeField] private  float invulnerableLength;

    #endregion

    #region Fields

    private bool _canRoll;
    private int _playerEnergy = InitialEnergy;
    private float invulnerableCounter;
    private PlayerAudioManager _audioManager;

    #endregion
    
    #region MonoBehaviour

    private void Start()
    {
        _audioManager = gameObject.GetComponent<PlayerAudioManager>();
    }

    private void Awake()
    {
        GameManager.PlayerDead += playerDead;
        GameManager.GameWon += HidePlayer;
    }

    private void OnDestroy()
    {
        GameManager.PlayerDead -= playerDead;
        GameManager.GameWon -= HidePlayer;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (invulnerableCounter > 0)
            {
                return;
            }

            enemyHit(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            if (invulnerableCounter > 0)
                return;
            enemyHit(other.gameObject);
        }
    }

    private void Update()
    {
        if (invulnerableCounter > 0)
        {
            invulnerableCounter -= Time.deltaTime;
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }

        if (invulnerableCounter <= 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    #endregion

    #region Methods

    private void enemyHit(GameObject other)
    {
        gameObject.GetComponent<Animator>().SetTrigger("PlayerHit");
        _playerEnergy -= playerHitDamage;
        if (_playerEnergy <= 0)
            GameManager.InvokePlayerDead();
        _audioManager.playPlayerHit();
        energyFrame.text = _playerEnergy.ToString();

        var player = GetComponent<PlayerMovement>();
        if (other.transform.position.x > transform.position.x)
            player.knockFromRight = true;
        else
            player.knockFromRight = false;
        player.knockBackCount = player.knockBackLength;

        invulnerableCounter = invulnerableLength;
    }

    public void addEnergy()
    {
        _playerEnergy += energyRestore;
        if (_playerEnergy >= 99)
            _playerEnergy = 99;
        energyFrame.text = _playerEnergy.ToString();
    }

    private void playerDead()
    {
        _playerEnergy = 0;
        gameObject.GetComponent<Animator>().SetTrigger("PlayerDead");
        _audioManager.playPlayerDead();
        Invoke("HidePlayer",1f);
    }

    private void HidePlayer()
    {
        gameObject.SetActive(false);
    }

    #endregion
}