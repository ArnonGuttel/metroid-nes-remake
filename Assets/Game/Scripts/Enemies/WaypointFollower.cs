using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed;
    public float delayCounter;

    #endregion

    #region Fields

    private int _waypointsArrayIndex;

    #endregion

    #region MonoBehaviour

    private void OnEnable()
    {
        _waypointsArrayIndex = 0;
    }

    private void Update()
    {
        if (delayCounter > 0)
        {
            delayCounter -= Time.deltaTime;
            return;
        }
        
        if (waypoints.Length == 0)
            return;
        
        if (transform.position == waypoints[_waypointsArrayIndex].transform.position)
        {
            _waypointsArrayIndex++;
            if (_waypointsArrayIndex == waypoints.Length)
                _waypointsArrayIndex = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[_waypointsArrayIndex].transform.position, speed * Time.deltaTime);
    }

    #endregion





}
