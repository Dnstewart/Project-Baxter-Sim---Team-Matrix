using UnityEngine;
/// <summary>
/// This Class is used to make the animations on the pedestrians work. 
/// made by team Matrix
/// </summary>
public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager Instance; /*!< an instance of PlayerManager. */

    void Awake ()
    {
        Instance = this;
    }

    #endregion
    public GameObject player; /*!< used to hold the pedestrian model with animations. */
}