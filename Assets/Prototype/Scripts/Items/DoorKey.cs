using UnityEngine;

public class DoorKey : MonoBehaviour
{
    #region Inspector

    public GameObject HiddenDoor;

    #endregion

    #region MonoBehaviour

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
            GameManager.KeyTaken(); // Let GameManager to invoke KeyTaken event
            gameObject.GetComponent<AudioSource>().Play(0);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, gameObject.GetComponent<AudioSource>().clip.length / 2f);
        }
    }

    #endregion

    #region Events

    private void removeDoor()
    {
        HiddenDoor.SetActive(false);
    }

    #endregion
}