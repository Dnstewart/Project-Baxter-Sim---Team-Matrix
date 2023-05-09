using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to control the weather systems in the simulation.
/// Made by Team Matrix
/// </summary>
public class GameManager : MonoBehaviour
{
    public int weatherType = 0; /*!< Variable representing the current weather type in the simulation. 0 = Clear; 1 = Raining; 2 = Snowing. */
    public int timeValue = 0; /*!< Variable representing the current time of day in the simulation. 0 = Day; 1 = Night. */
    public int simulationMode = 0; /*!< Variable representing the mode of the simulation. 0 = Baxter Entry; 1 = Baxter Exit. */

    /// <summary>
    /// This method calls the HandleWeather(), HandleTime(), and HandleSimMode() during the first frame.
    /// </summary>
    void Start()
    {
        HandleWeather();
        HandleTime();
        HandleSimMode();
    }

    /// <summary>
    /// This method decides the active weather in teh simulation.
    /// </summary>
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

    /// <summary>
    /// This method handles the time within the simulation.
    /// </summary>
    void HandleTime()
    {
        if (timeValue == 0)
        {

        }
        else
        {

        }
    }

    /// <summary>
    /// This method handles the mode that the simulation is in (we only have one currently).
    /// </summary>
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
