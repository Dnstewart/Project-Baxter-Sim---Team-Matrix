using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class needs to be attached to a camera object and it is used to move the camera around with w,a,s,d.
/// Made by Team Matrix
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 1f; /*!< The speed of the camera */
    [SerializeField]
    private float cameraSmoothing = 5f; /*!< Determines how smoothly the camera stops moving. */

    [SerializeField]
    private Vector2 cameraBounds = new (100, 100); /*!< how far the camera can move  */

    private Vector3 target; /*!< the position the camera is currently at. */
    private Vector3 cameraInput; /*!< the new location the camera will go to. */

    /// <summary>
    /// Updates the target variable with teh cameras position
    /// </summary>
    private void Awake()
    {
        target = transform.position;
    }

    /// <summary>
    /// used to update the cameras location.
    /// </summary>
    private void CameraInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 right = transform.right * x;
        Vector3 forward = transform.forward * z;

        cameraInput = (forward + right).normalized;
    }

    /// <summary>
    /// used to check if the given Vector3 position is within the given bounds.
    /// </summary>
    /// <param name="position">A Vector3 variable</param>
    /// <returns>a Boolean, true if within and false otherwise. </returns>
    private bool withinBounds(Vector3 position) 
    {
        return position.x > -cameraBounds.x && position.x < cameraBounds.x
            && position.z > -cameraBounds.y && position.z < cameraBounds.y;
    }

    /// <summary>
    /// This method is used to move the camera and check if the camera is within the bounds before it moves.
    /// </summary>
    private void Move()
    {
        Vector3 nextPosition = target + cameraInput * cameraSpeed;

        if (withinBounds(nextPosition))
        {
            target = nextPosition;
        }

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * cameraSmoothing);
    }

    /// <summary>
    /// draws gizmos to show the bounds of the camera.
    /// </summary>
    private void onDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 5f);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(cameraBounds.x * 2f, 5f, cameraBounds.y * 2f));
    }

    /// <summary>
    /// Update is called every frame, CameraInput() and Move() are called each time Update is called.
    /// </summary>
    private void Update()
    {
        CameraInput();
        Move();
    }
}
