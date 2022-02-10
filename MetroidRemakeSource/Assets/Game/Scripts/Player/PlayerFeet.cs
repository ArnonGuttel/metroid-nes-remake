using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    #region Fields

    private PlayerMovement _playerMovement;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Respawn"))
            GameManager.resetEnemies();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            _playerMovement.onGround = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            _playerMovement.onGround = false;
    }

    #endregion
}