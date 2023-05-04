using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ResourceManager is a class that is attached to a GameObject to control and keep track of different simulation elements.
/// </summary>
public class ResourceManager : MonoBehaviour
{
    // For the UI
    public Text timeText; /*!< stores a text UI object for the time. */
    public Text carCountText; /*!< stores a text UI object for the car count.  */
    public Text pedCountText; /*!< stores a text UI object for the pedestrian count.  */

    // Stored data
    public static float seconds; /*!< Stores the seconds */
    public static float minutes; /*!< Stores the Minutes */
    public int carCount; /*!< Stores the car count */
    public int pedCount; /*!< Stores the pedestrian count */
    public int pedLimit = 250;

    public static bool readyUp = false; /*!< acts as a flag for pausing and unpausing. */

    /// <summary>
    /// Start() is called before the first frame, Start() initalizes the global variables, calls ShowResources(), and PauseSim().
    /// </summary>
    public void Start()
    {
        seconds = 0f;
        minutes = 0;
        readyUp = false;
        PauseSim();
        ShowResources();
    }

    /// <summary>
    /// Every frame Update() checks if the p key was pressed and calls PauseSim() or ResumeSim() depending on the ready up flag.
    /// Also it updates the time variables, and calls ShowResources(). 
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (!readyUp)
            {
                ResumeSim();
                readyUp = true;
            }
            else
            {
                PauseSim();
                readyUp = false;
            }
            
        }

        seconds += Time.deltaTime;
        if (Math.Ceiling(seconds) % 61 == 0)
        {
            minutes++;
            seconds = 0f;
        }

        ShowResources();
    }

    /// <summary>
    /// Sets the global time scale to 0 to pause the simulation.
    /// </summary>
    public void PauseSim() 
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Sets the global time scale to 1 to resume the simulation.
    /// </summary>
    public void ResumeSim()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Updates the UI text.
    /// </summary>
    public void ShowResources()
    {
        timeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        carCountText.text = "Cars: " + carCount;
        pedCountText.text = "Pedestrians: " + pedCount;
    }
}
