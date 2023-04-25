using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverDetection : MonoBehaviour
{
    public float range = .5f;

    private CharacterNavigationController control;

    private float origMoveSpeed;

    private float stopTime = 0;

    public void Awake()
    {
        control = gameObject.GetComponent<CharacterNavigationController>();
        Debug.Log(gameObject.tag);
        if (gameObject.tag == "parkCar")
        {
            Debug.Log("hi");
            //this.origMoveSpeed = this.control.moveSpeed;
            //this.control.moveSpeed = 0;
            this.control.enabled = false;
        }
        else
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        GameObject[] peds = GameObject.FindGameObjectsWithTag("closePed");
        float shortestDistancePed = Mathf.Infinity;
        GameObject nearestPed = null;

        foreach (GameObject pedestrian in peds)
        {
            float distanceToPed = Vector3.Distance(transform.position, pedestrian.transform.position);

            if (distanceToPed < shortestDistancePed)
            {
                shortestDistancePed = distanceToPed;
                nearestPed = pedestrian;
            }
        }

        if (nearestPed != null && shortestDistancePed <= range)
        {
            this.stopTime = 1.5f + Time.fixedTime;
        }

        if (this.stopTime > Time.fixedTime)
        {
            //this.control.moveSpeed = this.origMoveSpeed;
            this.enabled = false;
            this.control.enabled = true;
            gameObject.tag = "Car";
        }
    }
}
