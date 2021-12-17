
using UnityEngine;

public class CameraFollows : MonoBehaviour
{

    public Transform Player;
    [SerializeField] private float cameraSpeed;
    private Vector3 _target;
    private GameObject _prevBoundary;

    void Update()
    {
        GameObject boundary = GameObject.Find("Boundary");
        followPlayer(boundary);
        if (boundary != _prevBoundary)
        {   
            if (_prevBoundary)
                GameManager.boundrayChange();
            if (_target != transform.position)
            {
                Player.GetComponent<PlayerMovement>().LockMovement = true;
                Player.GetComponent<PlayerFireScript>().canFire = false;
            }
            else
            {
                Player.GetComponent<PlayerMovement>().LockMovement = false;
                Player.GetComponent<PlayerFireScript>().canFire = true;
                _prevBoundary = boundary;
            }
        }
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

            
        }
    }
    
}
