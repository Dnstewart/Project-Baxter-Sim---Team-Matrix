using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointNav : MonoBehaviour
{
    //private NavMeshAgent controller;
    private CharacterNavigationController controller;
    public Waypoint currentWaypoint;

    public GameObject targetExit;

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

    private void findPedestrianExits()
    {
        GameObject[] pedestrianExits = GameObject.FindGameObjectsWithTag("exitPed");

        float shortestDistance = Mathf.Infinity;
        GameObject nearestExit = null;

        foreach (GameObject pedExit in pedestrianExits)
        {
            float exitDistance = Vector3.Distance(transform.position, pedExit.transform.position);

            if (exitDistance < shortestDistance)
            {
                shortestDistance = exitDistance;
                nearestExit = pedExit;
            }
        }

        targetExit = nearestExit;
    }

    private void findVehicleExits()
    {
        GameObject[] vehicleExits = GameObject.FindGameObjectsWithTag("exitCar");

        float shortestDistance = Mathf.Infinity;
        GameObject nearestExit = null;

        foreach (GameObject carExit in vehicleExits)
        {
            float exitDistance = Vector3.Distance(transform.position, carExit.transform.position);

            if (exitDistance < shortestDistance)
            {
                shortestDistance = exitDistance;
                nearestExit = carExit;
            }
        }

        targetExit = nearestExit;
    }

    private void selectBranch()
    {
        Waypoint closestBranch = null;
        float shortestDistance = Mathf.Infinity;

            foreach (Waypoint branch in currentWaypoint.branches)
            {
                float branchDistance = Vector3.Distance(branch.transform.position, targetExit.transform.position);

                if (branchDistance < shortestDistance)
                {
                    shortestDistance = branchDistance;
                    closestBranch = branch;
                }
            }

        currentWaypoint.nextWaypoint = closestBranch;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Car")
        {
            findVehicleExits();
        }
        else if (gameObject.tag == "Pedestrian")
        {
            findPedestrianExits();
        }


        if(controller.reachedDestination)
        {

            if(currentWaypoint == null)
            {
                Destroy(gameObject);
            }
            else
            {
                if(currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
                {
                    selectBranch();
                }

                currentWaypoint = currentWaypoint.nextWaypoint;
                controller.SetDestination(currentWaypoint.GetPosition());
            }
            
        }
    }
}
