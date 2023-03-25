using System;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    // For the UI
    public Text timeText;
    public Text carCountText;
    public Text pedCountText;

    // Stored data
    public static float seconds;
    public static float minutes;
    public static int carCount;
    public static int pedCount;

    // will start Sim
    public static bool readyUp = false;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0f;
        minutes = 0;
        readyUp = false;
        PauseSim();
        ShowResources();
    }

    // Update is called once per frame
    void Update()
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
    
    void PauseSim() 
    {
        Time.timeScale = 0;
    }
    void ResumeSim()
    {
        Time.timeScale = 1;
    }
    void ShowResources()
    {
        timeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        carCountText.text = "Cars: " + carCount;
        pedCountText.text = "Pedestrians: " + pedCount;
    }
}
