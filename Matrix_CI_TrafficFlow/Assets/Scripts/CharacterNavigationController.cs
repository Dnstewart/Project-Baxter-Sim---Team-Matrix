//Character Navigation Controller
/** 
 * This class makes models move to a specific location and is used as a component for objects.
 * Paired with the Waypoint Nav class it Changes locations.
 * Made by Team Matrix
 */

// IMPORTS
/** 
 * Main unity engine import. 
 */
using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{
    // VARIABLES

    /**
     * The flag for if a destination was reached.
     */
    public bool reachedDestination = false;

    /**
     * The Current location of an object.
     */
    private Vector3 loc;

    /**
     * The movment speed of an object
     */
    public float moveSpeed = 5.0f;

    /** 
     * The waypoint nav component reference.
     */
    private WaypointNav waypointCon;

    /**
     * The current destination.
     */
    private Vector3 destination;

    /** 
     * The stop distance from the destination.
     */
    public float stopDistance = .1f;

    /**
     * The object rotation speed.
     */
    public float rotationSpeed = 300f;

    /**
     * The flag for determining a pedestrian
     */
    public bool isPed = false;

    /**
     * The flag for determining a emergency service vehicle.
     */
    public bool isEmg = false;

    /** 
     * The range for detecting cars or peds.
     */
    [SerializeField]
    private float range = 3f;

    /**
     * The range for detecting emergency services.
     */
    [SerializeField]
    private float rangeEmg = 5f;

    /**
     * The time spent stopped
     */
    [SerializeField]
    private float stopTime = 0;

    /**
     * Testing variable
     */
    public Vector3 tempV3Ped;
    /**
    * Testing variables
    */
    public int countPed = 0;
    /**
    * Testing variables
    */
    public bool keepGoingTestPed = false;
    /**
    * Testing variables
    */
    public Vector3 tempV3Emg;
    /**
    * Testing variables
    */
    public int countEmg = 0;
    /**
    * Testing variables
    */
    public bool keepGoingTestEmg = false;

    // AWAKE
    /**
     * Upon object creation or simulation start the object 
     * will get inital information about location and waypoint navigation. 
     */
    public void Awake()
    {
        waypointCon = GetComponent<WaypointNav>();
        loc = transform.position;
        if (waypointCon.currentWaypoint == null)
        {

            Collider[] colliders = Physics.OverlapSphere(loc, .4f);
            foreach (Collider coll in colliders)
            {
                if (coll.gameObject.transform.tag == "Waypoint")
                {
                    waypointCon.currentWaypoint = coll.GetComponent<Waypoint>();
                    destination = coll.GetComponent<Waypoint>().GetPosition();
                    coll.enabled = false;
                    transform.LookAt(destination);
                }
            }
        }
    }

    //UPDATE
    /**
     * This method is called once every frame.
     * It does different actions based on the car and ped flags that are active.
     */
    public void Update()
    {
        /**
         * Get destination dstance and rotate model towards destination.
         * Check if the destination was reached. 
         */
        Vector3 destinationDirection = destination - transform.position;
        destinationDirection.y = 0;
        float destinationDistance = destinationDirection.magnitude;
        if (destinationDistance > stopDistance)
        {

            this.reachedDestination = false;
            Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            this.reachedDestination = true;
        }
        loc = transform.position;

        /**
         * check the isPed and isEmg flags to call correct subsequent method.
         */
        if (isPed)
        {
            pedestrianUpdate();
        }
        else if (isEmg)
        {
            //might need one 
        }
        else
        {
            carUpdate();
        }
    }

    // SET DESTINATION
    /**
    * A Method to set a new destination.
    * 
    * PARAM: Vector3 destination (The next destination to be set)
    */
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        this.reachedDestination = false;
    }

    // CARUPDATE
    /**
     * This method will stop a car when it detects pedestrians, 
     * emergency services, or other cars.
     */
    private void carUpdate()
    {
        /**
         * The setting of inital references and shortest distances for the nearby pedestrians and EMG's.
         */
        GameObject[] peds = GameObject.FindGameObjectsWithTag("Pedestrian");
        GameObject[] emgs = GameObject.FindGameObjectsWithTag("EmgServ");
        float shortestDistancePed = Mathf.Infinity;
        float shortestDistanceEmg = Mathf.Infinity;
        GameObject nearestPed = null;
        GameObject nearestEmg = null;

        foreach (GameObject pedestrian in peds)
        {
            float distanceToPed = Vector3.Distance(transform.position, pedestrian.transform.position);

            if (distanceToPed < shortestDistancePed)
            {
                shortestDistancePed = distanceToPed;
                nearestPed = pedestrian;

                if (!keepGoingTestPed)
                {
                    this.tempV3Ped = pedestrian.transform.position;
                    countPed = 0;
                }

            }
        }

        foreach (GameObject emg in emgs)
        {
            float distanceToEmg = Vector3.Distance(transform.position, emg.transform.position);

            if (distanceToEmg < shortestDistanceEmg)
            {
                shortestDistanceEmg = distanceToEmg;
                nearestEmg = emg;

                if (!keepGoingTestEmg)
                {
                    this.tempV3Emg = emg.transform.position;
                    countEmg = 0;
                }

            }
        }
        /**
         * Checks if a ped or emg is in range of the car and stops it for an aloted time.
         */
        if (shortestDistancePed < shortestDistanceEmg)
        {
            /**
             * Check pedestrian
             */
            if (nearestPed != null && shortestDistancePed <= range)
            {
                this.stopTime = 1.5f + Time.fixedTime;
                keepGoingTestPed = true;
            }
            else
            {
                keepGoingTestPed = false;
                countPed = 0;
            }
            if (this.stopTime > Time.fixedTime)
            {
                this.moveSpeed = 0;
                if (this.tempV3Ped.Equals(nearestPed.transform.position) && this.countPed < 5 && keepGoingTestPed)
                {
                    this.countPed++;
                }
                else if (this.countPed >= 5)
                {
                    this.moveSpeed = 2;
                }
            }
            else
            {
                this.moveSpeed = 2;
                this.countPed = 0;
                keepGoingTestPed = false;
            }
        }
        else
        {
            /**
             * Check emg services
             */
            if (nearestEmg != null && shortestDistanceEmg <= rangeEmg)
            {
                this.stopTime = 1f + Time.fixedTime;
                keepGoingTestEmg = true;
            }
            else
            {
                keepGoingTestEmg = false;
                countEmg = 0;
            }
            if (this.stopTime > Time.fixedTime)
            {
                this.moveSpeed = 0;
                if (this.tempV3Emg.Equals(nearestEmg.transform.position) && this.countEmg < 5 && keepGoingTestEmg)
                {
                    this.countEmg++;
                }
                else if (this.countEmg >= 5)
                {
                    this.moveSpeed = 2;
                }
            }
            else
            {
                this.moveSpeed = 2;
                this.countEmg = 0;
                keepGoingTestEmg = false;
            }
        }
    }

    // PEDESTRIAN UPDATE
    /**
    * This method will stop a ped when it detects emergency services.
    */
    private void pedestrianUpdate()
    {
        /**
        * The setting of inital references and shortest distances for the nearby EMG's.
        */
        GameObject[] emgs = GameObject.FindGameObjectsWithTag("EmgServ");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEmg = null;

        foreach (GameObject emg in emgs)
        {
            float distanceToEmg = Vector3.Distance(transform.position, emg.transform.position);

            if (distanceToEmg < shortestDistance)
            {
                shortestDistance = distanceToEmg;
                nearestEmg = emg;

                if (!keepGoingTestEmg)
                {
                    this.tempV3Emg = emg.transform.position;
                    countEmg = 0;
                }

            }
        }
        /**
        * Checks if a emg is in range of ped and stops it for an aloted time.
        */
        if (nearestEmg != null && shortestDistance <= rangeEmg)
        {
            this.stopTime = 2f + Time.fixedTime;
            keepGoingTestEmg = true;
        }
        else
        {
            keepGoingTestEmg = false;
            countEmg = 0;
        }
        if (this.stopTime > Time.fixedTime)
        {
            this.moveSpeed = 0;
            if (this.tempV3Emg.Equals(nearestEmg.transform.position) && this.countEmg < 5 && keepGoingTestEmg)
            {
                this.countEmg++;
            }
            else if (this.countEmg >= 5)
            {
                this.moveSpeed = 2;
            }
        }
        else
        {
            this.moveSpeed = 2;
            this.countEmg = 0;
            keepGoingTestEmg = false;
        }
    }
}
