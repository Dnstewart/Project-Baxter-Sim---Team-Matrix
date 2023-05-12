using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// WaypointNav is used to navigate a Waypoint network. 
/// For the purpose of the Baxter simulation, it is used in conjunction with CharacterNavigationController.
/// When an object with this script attached makes it to the target Waypoint in a network, WaypointNav will choose the next location.
/// Made by Team Matrix
/// </summary>
public class WaypointNav : MonoBehaviour
{
    //private NavMeshAgent controller; Were thinking abut using nav mesh agents thats why this is here.
    private CharacterNavigationController controller;
    public Waypoint currentWaypoint; /*!< The objects Current target waypoint. */

    public GameObject targetExit; /*!< An object that denotes a target exit. */

    public ParkingLot lotTarget; /*!< A parking lot destination. */

    private ResourceManager manager;
    private bool pedCloseFlag = false;
    public bool isAmbient = false;

    /// <summary>
    /// When the simulation is started Awake() is called
    /// </summary>
    private void Awake()
    {
        //controller = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterNavigationController>();
    }

    /// <summary>
    /// Start() is called after during the first frame of activation, if the object that the class is attached to 
    /// does not have a current waypoint then Start() will call findStartPoint() to get a current waypoint.
    /// Next Start() will call findPedestrianExits() or findVehicleExits() depending on the objects tag and find the closest exit to it.
    /// </summary>
    void Start()
    {
        if (currentWaypoint == null)
        {
            findStartPoint();
        }

        if (gameObject.tag == "Pedestrian" || gameObject.tag == "closePed")
        {
            findPedestrianExits();
        }

        if (gameObject.tag == "Car")
        {
            findVehicleExits();
        }
   
        controller.SetDestination(currentWaypoint.GetPosition());
        Vector3 direction = (currentWaypoint.GetPosition() - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ResourceManager>();
    }

    /// <summary>
    /// findStartPoint() finds all objects with the startPoint tag and select the closest one to the object.
    /// </summary>
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

    /// <summary>
    /// findPedestrianExits() finds all objects with the parkPed or exitPed tag and select the closest one to the object.
    /// </summary>
    private void findPedestrianExits()
    {
        GameObject[] pedestrianParking = GameObject.FindGameObjectsWithTag("parkPed");
        GameObject[] mapExits = GameObject.FindGameObjectsWithTag("exitPed");

        float shortestDistance = Mathf.Infinity;
        float exitDistance = 0;
        GameObject nearestExit = null;

        foreach (GameObject parking in pedestrianParking)
        {
            exitDistance = Vector3.Distance(transform.position, parking.transform.position);
            ParkingLot currentLot = parking.GetComponent<ParkingLot>();

            if (exitDistance < shortestDistance && currentLot.availability)
            {
                shortestDistance = exitDistance;
                nearestExit = parking;
            }
        }

        if (nearestExit != null)
        {
            this.lotTarget = nearestExit.GetComponent<ParkingLot>();
            this.lotTarget.assignedMembers++;
            this.targetExit = nearestExit;
        }
        else
        {
            foreach (GameObject exits in mapExits)
            {
                exitDistance = Vector3.Distance(transform.position, exits.transform.position);

                if (exitDistance < shortestDistance)
                {
                    shortestDistance = exitDistance;
                    nearestExit = exits;
                }
            }

        }

        this.targetExit = nearestExit;
    }

    /// <summary>
    /// findVehicleExits() finds all objects with the exitCar tag and select the closest one to the object.
    /// </summary>
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

    /// <summary>
    /// selectBranch() finds the branch path that is closest to the target exit.
    /// </summary>
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

    /// <summary>
    /// Update() is called every frame, Each frame Update(), for this class, sees where the object is in the 
    /// Waypoint network and finds the nest location it will go to. If the objects next Waypoint is null then 
    /// it will destroy the object when it arrives at the current waypoints location and update the ResourceManager.
    /// </summary>
    void Update()
    {

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
                if(gameObject.tag == "closePed" && !isAmbient)
                {
                    manager.pedCount--;

                    if (this.lotTarget != null)
                    {
                        this.lotTarget.outgoingMembers++;
                    }
                }
                else if (gameObject.tag == "Car" && !isAmbient)
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
                    Vector3 direction = (currentWaypoint.GetPosition() - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                }
            }
        }
    }
}
