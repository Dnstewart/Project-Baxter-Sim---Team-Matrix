using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// EnemyController is a solo class that just makes the animations for our pedestrians work.
/// Made by Team Matrix
/// </summary>
public class EnemyController : MonoBehaviour
{
    public float lookRadius = 8f;

    Transform target;
    NavMeshAgent agent;
    Animator animator;

    /// <summary>
    /// Start() initalizes the global variables.
    /// </summary>
    public void Start()
    {
        target = PlayerManager.Instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Update() activates the animations for each frame.
    /// </summary>
    public void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetFloat("forward", 1.0f);
        }
        else
        {
            agent.SetDestination(transform.position);
            animator.SetFloat("forward", 0.0f);
        }

        if (distance <= agent.stoppingDistance)
        {
            animator.SetFloat("forward", 0.0f);
            FaceTarget();
        }
    }

    /// <summary>
    /// Make the object face the target.
    /// </summary>
    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    /// <summary>
    /// Draws a sphere
    /// </summary>
    public void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}