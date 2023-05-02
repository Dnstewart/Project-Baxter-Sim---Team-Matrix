using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Waypoint class, 
/// This class is used to make a linked list of waypoints. 
/// There is a branch list to branch to a different waypoint in stead of the next waypoint. 
/// Made by Team Matrix
/// </summary>
public class Waypoint : MonoBehaviour
{
    public Waypoint previousWaypoint; /*!< The waypoint that came before the current waypoint, may be null. */
    public Waypoint nextWaypoint; /*!< The waypoint that follows the current waypoint, may be null. */

    public List<Waypoint> branches; /*!< A list of branch waypoints, may be an empty list. */

    [Range(0f, 1f)]
    public float branchRatio = 0.5f; /*!< Chance of taking a branch path if allowed in the waypoint nav class. */

    [Range(0f, 5f)]
    public float width = 1f; /*!< The width of the waypoint. (for the gizmos)  */

    /// <summary>
    /// GetPosition()
    /// Gets the position of the waypoint.
    /// </summary>
    /// <returns>A Vector3 variable witht the position of the waypoint.</returns>
    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * width / 2f;
        Vector3 maxBound = transform.position - transform.right * width / 2f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }

    /// <summary>
    /// GetNextWaypoint()
    /// Gets the next waypoint in the chain.
    /// </summary>
    /// <returns>A waypoint variable holding the next waypoint.</returns>
    public Waypoint GetNextWaypoint()
    {
        return this.nextWaypoint;
    }
}


