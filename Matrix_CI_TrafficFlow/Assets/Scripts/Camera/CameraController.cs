using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 1f;
    [SerializeField]
    private float cameraSmoothing = 5f;

    [SerializeField]
    private Vector2 cameraBounds = new (100, 100);

    private Vector3 target;
    private Vector3 cameraInput;

    private void Awake()
    {
        target = transform.position;
    }

    private void CameraInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 right = transform.right * x;
        Vector3 forward = transform.forward * z;

        cameraInput = (forward + right).normalized;
    }

    private bool withinBounds(Vector3 position) 
    {
        return position.x > -cameraBounds.x && position.x < cameraBounds.x
            && position.z > -cameraBounds.y && position.z < cameraBounds.y;
    }

    private void Move()
    {
        Vector3 nextPosition = target + cameraInput * cameraSpeed;

        if (withinBounds(nextPosition))
        {
            target = nextPosition;
        }

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * cameraSmoothing);
    }

    private void onDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 5f);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(cameraBounds.x * 2f, 5f, cameraBounds.y * 2f));
    }

    private void Update()
    {
        CameraInput();
        Move();
    }
}
