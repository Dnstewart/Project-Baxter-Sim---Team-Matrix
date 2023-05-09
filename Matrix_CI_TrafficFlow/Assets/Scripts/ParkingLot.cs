using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to operate the parking lot cars, when pedestrians get to a parking lot they are destroyed and enter an array,
/// then a car is spawned at the exit of the parking lot and they find a route out. 
/// Made by Team Matrix
/// </summary>
public class ParkingLot : MonoBehaviour
{

    public int lotCapacity = 500; /*!< number of cars allowed in the parkinglot. */
    public int assignedMembers = 0; /*!< Number of cars assigned to the parking lot. */
    public int outgoingMembers = 0; /*!< Number of cars ready to leave the parking lot. */

    public List<Waypoint> lotExit; /*!< List of cars ready to leave the parking lot. */
    public List<Waypoint> lotEntry; /*!< List of cars to enter the parking lot. */ 

    public bool availability = true; /*!< if there are spots available in the parking lot. */

    // Update is called once per frame
    void Update()
    {
        if (assignedMembers >= lotCapacity)
        {
            availability = false;
        }

    }

    void HandleEntry()
    {
        
    }

    
}