using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to control the weather systems in the simulation.
/// Made by Team Matrix
/// </summary>
public class EventManager : MonoBehaviour
{
    public int weatherType = 0; /*!< Variable representing the current weather type in the simulation. 0 = Clear; 1 = Raining; 2 = Snowing. */
    public int timeValue = 0; /*!< Variable representing the current time of day in the simulation. 0 = Day; 1 = Night. */

    public GameObject daylight; /*!< The light object in the scene */
    public GameObject rain; /*!< The rain objects that fall. */
    public GameObject snow; /*!< The snow objects that fall.  */

    /// <summary>
    /// This method calls the HandleWeather() and HandleTime() during the first frame.
    /// </summary>
    void Start()
    {
        HandleWeather();
        HandleTime();
    }

    /// <summary>
    /// This method decides the active weather in the simulation.
    /// </summary>
    void HandleWeather()
    {
        if (weatherType == 0)
        {
            rain.SetActive(false);
            snow.SetActive(false);
        }
        else if (weatherType == 1)
        {
            rain.SetActive(true);
            snow.SetActive(false);
        }
        else
        {
            rain.SetActive(false);
            snow.SetActive(true);
        }
    }

    /// <summary>
    /// This method handles the time of day within the simulation.
    /// </summary>
    void HandleTime()
    {
        if (timeValue == 0)
        {
            daylight.SetActive(true);
        }
        else
        {
            daylight.SetActive(false);
        }
    }
}
