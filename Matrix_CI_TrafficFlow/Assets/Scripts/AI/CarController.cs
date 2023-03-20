using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CarController : MonoBehaviour
{
    Rigidbody body;


    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float agility = 0.5f;
    [SerializeField]
    private float maxSpeed = 5;

    [SerializeField]
    private Vector2 movement;

    private float range = 2.5f;
    public float stopTime = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 movementInput)
    {
        this.movement = movementInput;
    }

    private void FixedUpdate()
    {
        if (body.velocity.magnitude < maxSpeed)
        {
            body.AddForce(movement.y * transform.forward * speed);
        }

        body.AddTorque(movement.x * Vector3.up * agility * movement.y);
    }

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
    
    private void Update()
    {
        pedestrianUpdate();
        if (this.stopTime > 0)
        {
            this.stopTime -= Time.deltaTime;
        }

    }


}