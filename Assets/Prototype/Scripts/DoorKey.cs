
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public GameObject HiddenDoor;

    private void Awake()
    {
        GameManager.OpenHiddenDoor += removeDoor;
    }

    private void OnDestroy()
    {
        GameManager.OpenHiddenDoor -= removeDoor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.KeyTaken();
            gameObject.GetComponent<AudioSource>().Play(0);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject,gameObject.GetComponent<AudioSource>().clip.length/2f);
        }
    }

    private void removeDoor()
    {
        HiddenDoor.SetActive(false);
    }
}
