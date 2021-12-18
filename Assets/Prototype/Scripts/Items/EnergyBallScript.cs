using UnityEngine;

public class EnergyBallScript : MonoBehaviour
{
    [SerializeField] private float EnergyBallTimer;

    private void Start()
    {
        Destroy(gameObject, EnergyBallTimer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().addEnergy();
            gameObject.GetComponent<AudioSource>().Play(0);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, gameObject.GetComponent<AudioSource>().clip.length / 3f);
        }
    }
}