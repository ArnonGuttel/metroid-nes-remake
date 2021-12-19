using UnityEngine;

public class GameWonBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.InvokeGameWon();
            gameObject.GetComponent<AudioSource>().Play(0);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, gameObject.GetComponent<AudioSource>().clip.length / 2f);
        }
    }
}