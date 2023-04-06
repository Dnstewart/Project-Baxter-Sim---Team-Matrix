using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 15f;
    [SerializeField]
    private float rotationSmoothing = 5f;

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
