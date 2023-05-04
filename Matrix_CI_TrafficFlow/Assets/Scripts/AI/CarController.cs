using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

/// <summary>
/// CarController was one of the early methods we used to move car objects around and detect pedestrians.
///  Made by Team Matrix
/// </summary>
public class CarController : MonoBehaviour
{
    Rigidbody body; /*!< The objects rigid body component. */


    [SerializeField]
    private float speed = 2; /*!< The base speed of the car. */
    [SerializeField]
    private float agility = 0.5f; /*!< The acceleration of the car. */
    [SerializeField]
    private float maxSpeed = 5; /*!< The max speed the car can get to. */

    [SerializeField]
    private Vector2 movement; /*!< The x and y location that the car will move to. */ 

    private float range = 2.5f; /*!< The distance from a pedestrian the car will stop at. */
    public float stopTime = 0; /*!< The amount of time the car will stop for a pedestrian */

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// This method changes the target location of the car.
    /// </summary>
    /// <param name="movementInput"> The target location for the car to move to.</param>
    public void Move(Vector2 movementInput)
    {
        this.movement = movementInput;
    }

    /// <summary>
    /// This method moves the car towards its target location.
    /// </summary>
    private void FixedUpdate()
    {
        if (body.velocity.magnitude < maxSpeed)
        {
            body.AddForce(movement.y * transform.forward * speed);
        }

        body.AddTorque(movement.x * Vector3.up * agility * movement.y);
    }

    /// <summary>
    /// This method scans for nearby pedestrians and stops the car if it gets to close to one.
    /// </summary>
    private void pedestrianUpdate()
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
            this.stopTime = .1f;

        }

        if (this.stopTime > 0)
        {
            this.speed = 0;
        }
        else
        {
            this.speed = 20;
        }
    }

    /// <summary>
    /// This method calls the other methods in this script every frame.
    /// </summary>
    private void Update()
    {
        pedestrianUpdate();
        if (this.stopTime > 0)
        {
            this.stopTime -= Time.deltaTime;
        }
        else
        {
            FixedUpdate();
        }

    }


}