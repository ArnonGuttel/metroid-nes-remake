using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GetComponent<AudioSource>().Play(0);
            GetComponent<Animator>().SetTrigger("OpenDoor");
        }
    }
}