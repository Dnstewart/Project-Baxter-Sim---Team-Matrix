using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is attached to a camera and used to rotate the camera.
/// Made by Team Matrix
/// </summary>
public class Rotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 15f; /*!< The speed at which the camera rotates */
    [SerializeField]
    private float rotationSmoothing = 5f; /*!< This variable decides how smooth the cameras rotation stops at. */

    private float target;
    private float current;

    private void Awake()
    {
        target = transform.eulerAngles.y;
        current = target;
    }

    private void CameraInput()
    {
        if (!Input.GetMouseButton(1)) return;
        target += Input.GetAxisRaw("Mouse X") * rotationSpeed;
    }

    private void Rotate()
    {
        current = Mathf.Lerp(current, target, Time.deltaTime * rotationSmoothing);
        transform.rotation = Quaternion.AngleAxis(current, Vector3.up);
    }

    private void Update()
    {
        CameraInput();
        Rotate();
    }
}
