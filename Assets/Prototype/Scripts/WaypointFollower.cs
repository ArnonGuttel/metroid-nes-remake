using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed;

    private int waypointsArrayIndex = 0;
    public float delayCounter;
    

    private void Update()
    {
        if (delayCounter > 0)
        {
            delayCounter -= Time.deltaTime;
            return;
        }
        
        if (waypoints.Length == 0)
            return;
        
        if (transform.position == waypoints[waypointsArrayIndex].transform.position)
        {
            waypointsArrayIndex++;
            if (waypointsArrayIndex == waypoints.Length)
                waypointsArrayIndex = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointsArrayIndex].transform.position, speed * Time.deltaTime);
    }
}
