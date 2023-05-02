using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int weatherType = 0; // Variable representing the current weather type in the simulation. 0 = Clear; 1 = Raining; 2 = Snowing.
    public int timeValue = 0; // Variable representing the current time of day in the simulation. 0 = Day; 1 = Night.
    public int simulationMode = 0; // Variable representing the mode of the simulation. 0 = Baxter Entry; 1 = Baxter Exit.

    void Start()
    {
        HandleWeather();
        HandleTime();
        HandleSimMode();
    }

    void HandleWeather()
    {
        if (weatherType == 0)
        {

        }
        else if (weatherType == 1)
        {

        }
        else
        {

        }
    }

    void HandleTime()
    {
        if (timeValue == 0)
        {

        }
        else
        {

        }
    }

    void HandleSimMode()
    {
        if (simulationMode == 0)
        {

        }
        else
        {

        }
    }
}
