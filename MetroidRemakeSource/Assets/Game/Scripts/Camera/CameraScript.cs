using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OpenHiddenDoor += CameraShake;
    }

    private void OnDestroy()
    {
        GameManager.OpenHiddenDoor -= CameraShake;
    }

    private void CameraShake()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Shake");
    }
}
