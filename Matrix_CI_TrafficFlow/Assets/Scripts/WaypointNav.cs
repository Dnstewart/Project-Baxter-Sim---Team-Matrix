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

    private ResourceManager manager;
    private bool pedCloseFlag = false;

    private void Awake()
    {
        //controller = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterNavigationController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (currentWaypoint == null)
        {
            findStartPoint();
        }
   
        controller.SetDestination(currentWaypoint.GetPosition());
        gameObject.transform.LookAt(currentWaypoint.GetPosition());

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ResourceManager>();
    }

    private void findStartPoint()
    {
        GameObject[] startingPoints = GameObject.FindGameObjectsWithTag("startPoint");

        float shortestDistance = Mathf.Infinity;
        GameObject nearestStart = null;
        foreach (GameObject point in startingPoints)
        {
            float startDistance = Vector3.Distance(transform.position, point.transform.position);

            if (startDistance < shortestDistance)
            {
                shortestDistance = startDistance;
                nearestStart = point;
            }
        }
        this.currentWaypoint = nearestStart.GetComponent<Waypoint>();
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

        this.targetExit = nearestExit;
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

        this.targetExit = nearestExit;
    }

    private void selectBranch()
    {
        Waypoint closestBranch = null;
        float shortestDistance = Mathf.Infinity;
        foreach (Waypoint branch in currentWaypoint.branches)
        {
            float branchDistance = Vector3.Distance(branch.transform.position, this.targetExit.transform.position);

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
        if (gameObject.tag == "Car")
        {
            findVehicleExits();
        }
        else if (gameObject.tag == "Pedestrian" || gameObject.tag == "closePed")
        {
            findPedestrianExits();
        }

        if (currentWaypoint != null && currentWaypoint.nextWaypoint == null && !pedCloseFlag)
        {
            //test
            if (gameObject.tag == "Pedestrian")
            {
                gameObject.GetComponent<CharacterNavigationController>().moveSpeed = 0;
                gameObject.tag = "closePed";
                pedCloseFlag = true;
            }
            //test
        }

        if (controller.reachedDestination)
        {
            
            if (currentWaypoint == null)
            {
                if(gameObject.tag == "closePed")
                {
                    manager.pedCount--;
                }
                else if (gameObject.tag == "Car")
                {
                    manager.carCount--;
                }
                Destroy(gameObject);
            }
            else
            {
                if(currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
                {
                    selectBranch();
                }

                currentWaypoint = currentWaypoint.nextWaypoint;
                if(currentWaypoint != null)
                {
                    controller.SetDestination(currentWaypoint.GetPosition());
                }
            }
        }
    }
}
