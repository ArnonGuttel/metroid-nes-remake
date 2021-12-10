using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    private BoxCollider2D cameraBox;
    public Transform Player;
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    
    private void Start()
    {
        cameraBox = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        GameObject boundary = GameObject.Find("Boundary");
        Vector3 prevPosition = transform.position;
        followPlayer(boundary);
        Vector3 temp = transform.position;
        if (boundary.gameObject.GetComponentInParent<BoundaryManager>().cameraLockX)
        {
            temp.x = prevPosition.x;
        }
        if (boundary.gameObject.GetComponentInParent<BoundaryManager>().cameraLockY)
        {
            temp.y = prevPosition.y;
        }

        transform.position = temp;
    }

    private void followPlayer(GameObject boundary)
    {
        if (boundary)
        {
            BoxCollider2D boundaryCollider = boundary.GetComponent<BoxCollider2D>();
            transform.position = new Vector3(
                Mathf.Clamp(Player.position.x, boundaryCollider.bounds.min.x + cameraBox.size.x/2, boundaryCollider.bounds.max.x - cameraBox.size.x/2),
                Mathf.Clamp(Player.position.y-2, boundaryCollider.bounds.min.y + cameraBox.size.y/2, boundaryCollider.bounds.max.y - cameraBox.size.y/2),
                -10);
        }
    }
}
