using UnityEngine;

public class Enemy2BulletScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}