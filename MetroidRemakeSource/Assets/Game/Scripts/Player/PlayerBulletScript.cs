using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;
        if (other.CompareTag("Platform") || other.CompareTag("Enemy") || other.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }
}