using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingLot : MonoBehaviour
{

    public int lotCapacity = 500;
    public int assignedMembers = 0;
    public int outgoingMembers = 0;

    public List<Waypoint> lotExit;
    public List<Waypoint> lotEntry;

    public bool availability = true;

    // Update is called once per frame
    void Update()
    {
        if (assignedMembers >= lotCapacity)
        {
            availability = false;
        }
    }

    void HandleOutgoing()
    {
        if (outgoingMembers > 0)
        {

        }
    }

    
}