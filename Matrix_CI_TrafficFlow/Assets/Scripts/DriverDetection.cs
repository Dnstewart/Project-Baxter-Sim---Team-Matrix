using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverDetection : MonoBehaviour
{
    // Update is called once per frame
    /*void Update()
    {
        GameObject[] peds = GameObject.FindGameObjectsWithTag("Pedestrian");
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
        else
        {
            countPed = 0;
        }
        if (this.stopTime > Time.fixedTime)
        {
            this.moveSpeed = 0;
            if (this.countPed < 5 && keepGoingTestPed)
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
        }
    }*/
}
