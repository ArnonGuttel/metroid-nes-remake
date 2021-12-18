using UnityEngine;

public class RollPowerUpScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().rollPowerUp = true;
            gameObject.GetComponent<AudioSource>().Play(0);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, gameObject.GetComponent<AudioSource>().clip.length / 2f);
        }
    }
}