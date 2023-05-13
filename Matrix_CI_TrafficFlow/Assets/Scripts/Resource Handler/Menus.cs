using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class contains methods that are used to change scenes. each method also resets Time.timeScale to 0.
/// </summary>
public class Menus : MonoBehaviour
{
    /// <summary>
    /// This method loads the Menu scene.
    /// </summary>
    public void ReturnToMain()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 0;
    }
    /// <summary>
    /// This method loads the Explanation scene.
    /// </summary>
    public void GoToExplanation()
    {
        SceneManager.LoadScene("Explanation");
        Time.timeScale = 0;
    }
    /// <summary>
    /// This method loads the Main scene.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 0;
    }
}
