using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    #region Inspector

    [SerializeField] private float cameraSpeed;
    [SerializeField] private Transform Player;
    
    #endregion

    #region Fields

    private Vector3 _target;
    private GameObject _prevBoundary;

    #endregion

    #region MonoBehaviour

    void Update()
    {
        GameObject boundary = GameObject.Find("Boundary");
        followPlayer(boundary);
        if (boundary != _prevBoundary)
            CameraBoundaryChange(boundary);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, cameraSpeed * Time.deltaTime);
    }

    #endregion


    #region Methods

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

    private void CameraBoundaryChange(GameObject boundary)
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

    #endregion
}