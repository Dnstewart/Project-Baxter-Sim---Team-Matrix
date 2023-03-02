using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarAI : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> path = null;
    [SerializeField]
    private float targetDistance = .3f, finalDistance = .1f;
    [SerializeField]
    private float turningAngleOffset = 5;
    [SerializeField]
    private Vector3 currentTargetPosition;

    private int index = 0;

    private bool stop;

    public bool Stop
    {
        get { return stop; }
        set { stop = value; }
    }
    
    [field: SerializeField]
    public UnityEvent<Vector2> OnDrive { get; set; }

    private void Start()
    {
        if (path == null || path.Count == 0)
        {
            Stop = true;
        }
        else
        {
            currentTargetPosition = path[index];
        }
    }

    public void CreatePath(List<Vector3> path)
    {
        if(path.Count == 0)
        {
            Destroy(gameObject);
            return;
        }
        this.path = path;
        index = 0;
        currentTargetPosition = this.path[index];

        Vector3 relativeWaypoint = transform.InverseTransformPoint(this.path[index + 1]);

        float angle = Mathf.Atan2(relativeWaypoint.x, relativeWaypoint.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle, 0);
        Stop = false;
    }

    private void Update()
    {
        CheckIfArrived();
        Drive();
    }

    private void Drive()
    {
        if (stop)
        {
            OnDrive?.Invoke(Vector2.zero);
        }
        else
        {
            Vector3 relativeWaypoint = transform.InverseTransformPoint(currentTargetPosition);
            float angle = Mathf.Atan2(relativeWaypoint.x, relativeWaypoint.z) * Mathf.Rad2Deg;
            var rotateCar = 0;
            if (angle > turningAngleOffset)
            {
                rotateCar = 1;
            }
            else if (angle < -turningAngleOffset)
            {
                rotateCar = -1;
            }
            OnDrive?.Invoke(new Vector2(rotateCar, 1));
        }
    }

    private void CheckIfArrived()
    {
        if (stop == false)
        {
            var distanceToCheck = targetDistance;
            if (index == path.Count - 1)
            {
                distanceToCheck = finalDistance;
            }
            
            if (Vector3.Distance(currentTargetPosition, transform.position) < distanceToCheck)
            {
                SetNextTarget();
            }
        }
    }

    private void SetNextTarget()
    {
        index++;
        if (index >= path.Count)
        {
            stop = true;
            Destroy(gameObject);
        }
        else
        {
            currentTargetPosition = path[index];
        }
    }
}
