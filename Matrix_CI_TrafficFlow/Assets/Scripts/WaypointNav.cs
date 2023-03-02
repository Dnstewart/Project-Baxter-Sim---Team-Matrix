using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointNav : MonoBehaviour
{
    //private NavMeshAgent controller;
    private CharacterNavigationController controller;
    public Waypoint currentWaypoint;

    private void Awake()
    {
        //controller = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterNavigationController>();
    }
    // Start is called before the first frame update
    void Start()
    {

      controller.SetDestination(currentWaypoint.GetPosition());
        gameObject.transform.LookAt(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.reachedDestination)
        {
            if(currentWaypoint == null)
            {
                Destroy(gameObject);
            }
            currentWaypoint = currentWaypoint.nextWaypoint;
            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
