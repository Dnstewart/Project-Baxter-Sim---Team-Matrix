using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointNav : MonoBehaviour
{
     NavMeshAgent controller;
    public Waypoint currentWaypoint;

    private void Awake()
    {
        controller = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        controller.destination = currentWaypoint.GetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.remainingDistance <= 0.01)
        {
            currentWaypoint = currentWaypoint.nextWaypoint;
            controller.destination = currentWaypoint.GetPosition();
        }
    }
}
