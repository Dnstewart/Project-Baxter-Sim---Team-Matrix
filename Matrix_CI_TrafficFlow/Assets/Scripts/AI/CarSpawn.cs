using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for spawning cars from parking lots or onto streets. This separate from the other spawner class.
/// Made by Team Matrix
/// </summary>
public class CarSpawn : MonoBehaviour
{
    [SerializeField]
    public GameObject[] cars;/*!< A list of different cars that spawn form the parking lots. */
    [SerializeField]
    public float time = 1f; /*!< The time between each car spawn. */
    public float countdown = 3f; /*!< The timer before the next car can spawn. */

    public bool isAmbient = false; /*!< A flag that determines if a car spawner is used for a parking lot or a normal street spawner. */

    public ResourceManager manager; /*!< holds an instance of the resource manager. */
    public ParkingLot assignedLot; /*!< holds an instance of the associated parking lot. */

    /// <summary>
    /// Start is called before the first frame update, it finds the Resource manager object and puts it in the manager variable.
    /// </summary>
    public void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ResourceManager>();
    }

    /// <summary>
    /// Update is called each frame, it checks wahat kind of spawner it is and either calls Handle outgoing if it is a parking lot spawner 
    /// or starts a coroutine if it is a regular spawner.  
    /// </summary>
    void Update()
    {
        if (countdown <= 0)
        {
            if (!isAmbient)
            {
                HandleOutgoing();
                countdown = 3f;
            }
            else
            {
                StartCoroutine(SpawnCar(time, cars));
                countdown = 3f;
            }
        }

        countdown -= Time.deltaTime;
    }


    // Spawns cars
    private IEnumerator SpawnCar(float interval, GameObject[] cars)
    {
        yield return new WaitForSeconds(interval);
        GameObject newCar = Instantiate(cars[Random.Range(0, 6)], gameObject.transform.position, gameObject.transform.rotation);

        if (!isAmbient)
        {
            manager.carCount++; 
        }
          
    }

    /// <summary>
    /// checks if there are cars waiting to leave the parking lot and starts spawning if so.
    /// </summary>
    public void HandleOutgoing()
    {
        if (assignedLot.outgoingMembers > 0)
        {
            StartCoroutine(SpawnCar(time, cars));
            assignedLot.outgoingMembers --;
        }
    }
}
