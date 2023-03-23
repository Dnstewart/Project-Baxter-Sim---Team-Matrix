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
    public float rotationSpeed = 0.5f;
    public float rotationSpeed = 0f;

    [SerializeField]
    private float range = 3f;
    [SerializeField]
    private float stopTime = 0;
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
            //pedestrianUpdate();
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
        float shortestDistance = Mathf.Infinity;
        GameObject nearestPed = null;

        foreach (GameObject pedestrian in peds)
        {
            float distanceToPed = Vector3.Distance(transform.position, pedestrian.transform.position);

            if (distanceToPed < shortestDistance)
            {
                shortestDistance = distanceToPed;
                nearestPed = pedestrian;
            }
        }

        if (nearestPed != null && shortestDistance <= range)
        {
            this.stopTime = 3f + Time.fixedTime;
        }
        if (this.stopTime > Time.fixedTime)
        {
            this.moveSpeed = 0;

        }
        else
        {
            this.moveSpeed = 2;

        }
    }

    private void pedestrianUpdate()
    {

    } 
}
