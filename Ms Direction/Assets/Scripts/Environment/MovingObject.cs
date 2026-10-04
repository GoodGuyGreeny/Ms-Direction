using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE:
// This script is somewhat based upon Brian Winn's script in MI445.
// I think it's far more robust and modular in its implementation, 
// but still, some credit must go to Michigan State University.

public class MovingObject : MonoBehaviour
{
    

    [Header("Movement Settings")]
    [Tooltip("Should this object be moving upon instantiation (if so, set true), or will you have another object set it to move? (if so, set false)")]
    public bool moveUponSpawn = true;

    public enum EndOfPathBehavior
    {
        Looping,
        Reversing,
        Stop
    }

    [Tooltip("Defines the end of path behavior for this moving object." +
        "\n\nLooping: Once the final waypoint has been reached, navigate towards the first waypoint in the array" +
        "\n\nReversing: Once the final waypoint has been reached, navigate backwards through each previous waypoint until the first waypoint is reached." +
        "\n\nStop: Once the final waypoint has been reached, movement ends.")]
    public EndOfPathBehavior endOfPathBehavior = EndOfPathBehavior.Looping;

    [Header("Waypoint Array")]
    public Waypoint[] waypoints;

    private Waypoint currentWaypoint;
    private Waypoint nextWaypoint;

    // Whether or not the platform is going to move at all
    private bool moving = true;
    // Whether or not we're in the waiting stage for a specific waypoint
    private bool wait = true;
    // Whether or not designers were stupid
    private bool designerWasStupidAndDidntSupplyAnyWaypoints = false;
    // Whether or not we're reversing
    private bool reversing = false;

    private int nextWaypointIndex = 0;
    private float percentMoved = 0;
    private float timeSpentWaiting = 0;

    // These variables may seem redundant, as we can pull this data
    // from our waypoint objects easily, but it helps for the Reverse mode
    // of EndOfPathBehavior.
    private float currentWaitTime = 0;
    private float currentSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Length > 1)
        {
            transform.position = waypoints[0].transform.position;
            currentWaypoint = waypoints[0];

            nextWaypoint = waypoints[1];
            nextWaypointIndex = 1;

            currentSpeed = currentWaypoint.speed;
            currentWaitTime = currentWaypoint.waitTime;
        }
        else if(waypoints.Length == 1)
        {
            Debug.LogWarning("WARNING in MovingObject.cs attached to " + this.gameObject.name + ". Only one waypoint supplied. No effect.");
            currentWaypoint = waypoints[0];
            nextWaypoint = waypoints[0];
            nextWaypointIndex = 0;
        }
        else
        {
            Debug.LogError("ERROR in MovingObject.cs attached to " + this.gameObject.name + ". No waypoints supplied.");
            designerWasStupidAndDidntSupplyAnyWaypoints = true;
        }

        moving = moveUponSpawn;
    }

    private void FixedUpdate()
    {
        if (designerWasStupidAndDidntSupplyAnyWaypoints || !moving)
            return;

        if(wait)
            Wait();
        else
        {
            Move();
            SwitchWaypoints();
        }
    }

    void Wait()
    {
        if (wait && timeSpentWaiting < currentWaitTime)
        {
            timeSpentWaiting += Time.deltaTime;
        }
        else
        {
            timeSpentWaiting = 0;
            wait = false;
        }
    }

    void Move()
    {
        percentMoved += Time.deltaTime * currentSpeed;
        transform.position = Vector3.Lerp(currentWaypoint.transform.position, nextWaypoint.transform.position, percentMoved);
    }

    void SwitchWaypoints()
    {
        if(percentMoved >= 1)
        {
            switch (endOfPathBehavior)
            {
                case EndOfPathBehavior.Looping:
                    SwitchWaypointsLooping();
                    break;
                case EndOfPathBehavior.Reversing:
                    SwitchWaypointsReversing();
                    break;
                case EndOfPathBehavior.Stop:
                    SwitchWaypointsStop();
                    break;
            }
            
        }
    }

    void SwitchWaypointsLooping()
    {
        currentWaypoint = nextWaypoint;

        nextWaypointIndex++;
        if (waypoints.Length == nextWaypointIndex)
        {
            nextWaypointIndex = 0;
        }

        nextWaypoint = waypoints[nextWaypointIndex];
        currentSpeed = currentWaypoint.speed;
        currentWaitTime = currentWaypoint.waitTime;

        wait = true;
        percentMoved = 0;
    }

    void SwitchWaypointsReversing()
    {
        currentWaypoint = nextWaypoint;

        if (reversing)
        {
            nextWaypointIndex--;
            if (nextWaypointIndex == -1)
            {
                // We just decreased it by 1, so increasing by two gives us the NEXT waypoint
                // We do have to check we aren't going out of bounds, though
                nextWaypointIndex += 2;
                if (waypoints.Length == nextWaypointIndex)
                {
                    nextWaypointIndex = 0;
                }
                reversing = false;
            }
        }
        else
        {
            nextWaypointIndex++;
            if (waypoints.Length == nextWaypointIndex)
            {
                // We just increased it by 1, so decreasing by two gives us the PREVIOUS waypoint
                // We do have to check we aren't going out of bounds, though
                nextWaypointIndex -= 2;
                if (nextWaypointIndex < 0)
                {
                    nextWaypointIndex = 0;
                }
                reversing = true;
            }
        }

        nextWaypoint = waypoints[nextWaypointIndex];

        if(reversing)
        {
            currentSpeed = nextWaypoint.speed;
            currentWaitTime = nextWaypoint.waitTime;
        }
        else
        {
            currentSpeed = currentWaypoint.speed;
            currentWaitTime = currentWaypoint.waitTime;
        }

        wait = true;
        percentMoved = 0;
    }

    void SwitchWaypointsStop()
    {
        currentWaypoint = nextWaypoint;

        nextWaypointIndex++;
        if (waypoints.Length == nextWaypointIndex)
        {
            moving = false;
            nextWaypointIndex = 0;
        }

        nextWaypoint = waypoints[nextWaypointIndex];
        currentSpeed = currentWaypoint.speed;
        currentWaitTime = currentWaypoint.waitTime;

        wait = true;
        percentMoved = 0;
    }

    public void SetMoving(bool shouldMove)
    {
        moving = shouldMove;
    }

}

[System.Serializable]
public class Waypoint
{
    [Tooltip("The position of this waypoint in space")]
    public Transform transform;
    [Tooltip("The speed that the moving object should take moving FROM this waypoint to the next.")]
    public float speed;
    [Tooltip("The amount of time that the moving object should wait at this waypoint")]
    public float waitTime = 0;
}
