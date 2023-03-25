using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{
    public bool reachedDestination = false;

    private Vector3 loc;
    public float moveSpeed = 5.0f;
    private WaypointNav waypointCon;
    private Vector3 destination;
    public float stopDistance = .1f;
    public float rotationSpeed = 0.5f;
    public bool isPed = false;
    public bool isEmg = false;

    [SerializeField]
    private float range = 3f;
    [SerializeField]
    private float rangeEmg = 5f;
    [SerializeField]
    private float stopTime = 0;
    // these are for testing a car to ignore a pedestiran not moving and for it to keep going
    public Vector3 tempV3Ped;
    public int countPed = 0;
    public bool keepGoingTestPed = false;

    public Vector3 tempV3Emg;
    public int countEmg = 0;
    public bool keepGoingTestEmg = false;
    // Start is called before the first frame update

    private void Awake()
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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        this.reachedDestination = false;
    }

    private void carUpdate()
    {
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
        if (shortestDistancePed < shortestDistanceEmg)
        { 
           // check pedestrian
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
            // check emg services
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

    private void pedestrianUpdate()
    {
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

    void StopPed()
    {

    }
}
