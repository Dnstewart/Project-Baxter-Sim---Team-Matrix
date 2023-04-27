using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to detect a pedestrian and is intended to be used with an object with a parkCar tag in the inspector, 
/// when a pedestrian is in range it will cause a parkCar to become a Car and activate CharacterNavigationController.
/// </summary>
public class DriverDetection : MonoBehaviour
{
    public float range = .5f; /*!< The detection range for the car. */

    private CharacterNavigationController control; /*!< An instance of the Character navigation controller component. */

    private float stopTime = 0; /*!< The time that Time.fixedTime needs to get to before an object can continue.*/

    /// <summary>
    /// When the simulation is started Awake() is called, this method checks if the object that the class component is connected to has the parkCar tag.
    /// If it does it disables the Character Navigation Controller component, if not it disables the Driver Detection component.
    /// </summary>
    public void Awake()
    {
        control = gameObject.GetComponent<CharacterNavigationController>();
        if (gameObject.tag == "parkCar")
        {
            this.control.enabled = false;
        }
        else
        {
            this.enabled = false;
        }
    }

    /// <summary>
    /// Update() is called every frame, Each frame Update() for this class checks if a pedestrian with the closePed tag is in range. 
    /// If one is then this class component is disabled, the character navigation controller is enabled, and the object tag is changed to Car.
    /// </summary>
    public void Update()
    {
        GameObject[] peds = GameObject.FindGameObjectsWithTag("closePed");
        float shortestDistancePed = Mathf.Infinity;
        GameObject nearestPed = null;

        // find the distance between the car and pedestrian.
        foreach (GameObject pedestrian in peds)
        {
            float distanceToPed = Vector3.Distance(transform.position, pedestrian.transform.position);

            if (distanceToPed < shortestDistancePed)
            {
                shortestDistancePed = distanceToPed;
                nearestPed = pedestrian;
            }
        }

        // If a pedestrian is in range the car starts to move and this class is disabled as it is no longer needed.
        if (nearestPed != null && shortestDistancePed <= range)
        {
            this.stopTime = 1.5f + Time.fixedTime;
        }

        if (this.stopTime > Time.fixedTime)
        {
            this.enabled = false;
            this.control.enabled = true;
            gameObject.tag = "Car";
        }
    }
}
