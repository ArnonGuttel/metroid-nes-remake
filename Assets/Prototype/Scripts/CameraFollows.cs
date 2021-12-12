using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    private BoxCollider2D cameraBox;
    public Transform Player;
    [SerializeField] private float cameraSpeed;
    // [SerializeField] private bool lockX;
    // [SerializeField] private bool lockY;
    
    private Vector3 _target;
    private void Start()
    {
        cameraBox = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        GameObject boundary = GameObject.Find("Boundary");
        // Vector3 prevPosition = transform.position;
        followPlayer(boundary);
        
        // Vector3 temp = transform.position;
        // if (boundary.gameObject.GetComponentInParent<BoundaryManager>().cameraLockX)
        // {
        //     temp.x = prevPosition.x;
        // }
        // if (boundary.gameObject.GetComponentInParent<BoundaryManager>().cameraLockY)
        // {
        //     temp.y = prevPosition.y;
        // }
        //
        // transform.position = temp;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,_target,cameraSpeed*Time.deltaTime);
    }

    private void followPlayer(GameObject boundary)
    {
        if (boundary)
        {
            Vector3 playerPosition = Player.transform.position;
            BoundaryManager script = boundary.GetComponentInParent<BoundaryManager>();
            if (script.XMaxEnabled && script.XMinEnabled)
                _target.x = Mathf.Clamp(playerPosition.x, script.XMinValue, script.XMaxValue);
            else if (script.XMaxEnabled)
                _target.x = Mathf.Clamp(playerPosition.x, playerPosition.x, script.XMaxValue);
            else if (script.XMinEnabled)
                _target.x = Mathf.Clamp(playerPosition.x, script.XMinValue, playerPosition.x);
            
            if (script.YMaxEnabled && script.YMinEnabled)
                _target.y = Mathf.Clamp(playerPosition.y, script.YMinValue, script.YMaxValue);
            else if (script.YMaxEnabled)
                _target.y = Mathf.Clamp(playerPosition.y, playerPosition.y, script.XMaxValue);
            else if (script.YMinEnabled)
                _target.y = Mathf.Clamp(playerPosition.y, script.XMinValue, playerPosition.y);
            
            _target.z = -10;
            
            // BoxCollider2D boundaryCollider = boundary.GetComponent<BoxCollider2D>();
            // _target = new Vector3(
            //     Mathf.Clamp(Player.position.x, boundaryCollider.bounds.min.x + cameraBox.size.x/2, boundaryCollider.bounds.max.x - cameraBox.size.x/2),
            //     Mathf.Clamp(Player.position.y-2, boundaryCollider.bounds.min.y + cameraBox.size.y/2, boundaryCollider.bounds.max.y - cameraBox.size.y/2),
            //     -10);
            
        }
    }
}
