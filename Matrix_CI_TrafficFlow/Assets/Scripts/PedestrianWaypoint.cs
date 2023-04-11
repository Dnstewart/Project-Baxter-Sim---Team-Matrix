using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianWaypoint : MonoBehaviour
{
    private CharacterNavigationController controller;
    public Waypoint currentWaypoint;

    public GameObject targetExit;

    private void Awake()
    {
        controller = GetComponent<CharacterNavigationController>();
    }

    void Start()
    {
        controller.SetDestination(currentWaypoint.GetPosition());
        gameObject.transform.LookAt(currentWaypoint.GetPosition());
    }

    
}