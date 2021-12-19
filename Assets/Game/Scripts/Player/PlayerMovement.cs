using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Inspector

    public Rigidbody2D rb;
    public Animator Animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float ShortJumpDecrease;
    [SerializeField] private LayerMask mask;

    #endregion

    #region Fields

    private BoxCollider2D playerFeetCollision2D;
    [HideInInspector] public bool LockMovement;
    [HideInInspector] public float knockBackCount;
    [HideInInspector] public bool knockFromRight;
    [HideInInspector] public bool onGround;
    [HideInInspector] public bool PlayerDead;
    public float knockBackLength;
    public float knockBack;
    public bool rollPowerUp;
    private bool _isRolling;
    private PlayerAudioManager _audioManager;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        _audioManager = gameObject.GetComponent<PlayerAudioManager>();
    }

    void Update()
    {
        if (PlayerDead || LockMovement)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (LockMovement)
                Animator.SetBool("IsWalking", false);
            return;
        }

        if (knockBackCount > 0)
            KnockBackPlayer();

        float dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);

        if (Input.GetKey(KeyCode.UpArrow) && _isRolling)
            stopRoll();

        if (Input.GetKeyDown(KeyCode.Space) && onGround) // long jump
            doLongJump();

        else if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0.0f) // short jump
            rb.velocity = new Vector2(rb.velocity.x, math.max(rb.velocity.y - ShortJumpDecrease, 0));

        if (Input.GetKey(KeyCode.DownArrow) && rollPowerUp && onGround)
            startRoll();

        // Check and play the current animation 
        if (!_isRolling)
        {
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.GetComponent<PlayerFireScript>().canFire = true;
            Animator.SetBool("IsRolling", false);
        }

        if (onGround && rb.velocity.magnitude >= 0.001f)
            Animator.SetBool("IsWalking", true);
        else
            Animator.SetBool("IsWalking", false);

        Animator.SetBool("IsJumping", !onGround);
    }

    #endregion

    #region Methods

    private void startRoll()
    {
        if (!_isRolling)
        {
            _isRolling = true;
            gameObject.GetComponent<PlayerFireScript>().canFire = false;
            Animator.SetBool("IsRolling", true);
        }
    }

    private void stopRoll()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 1f, mask.value);
        if (hit.collider == null)
            _isRolling = false;
    }

    private void KnockBackPlayer()
        // This method will be called whenever the player git hit from an enemy.
    {
        if (knockFromRight)
        {
            if (_isRolling)
                rb.velocity = new Vector2(-knockBack, 0);
            else
                rb.velocity = new Vector2(-knockBack, knockBack);
        }

        if (!knockFromRight)
        {
            if (_isRolling)
                rb.velocity = new Vector2(knockBack, 0);
            else
                rb.velocity = new Vector2(knockBack, knockBack);
        }

        knockBackCount -= Time.deltaTime;
    }

    private void doLongJump()
    {
        if (_isRolling)
        {
            stopRoll();
            return;
        }

        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        _audioManager.playJump();
    }

    #endregion
}