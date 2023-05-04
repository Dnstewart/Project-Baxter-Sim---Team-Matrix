using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawner is a class that attaches to an object and spawns an child object designated in the inspector.
/// Made by Team Matrix
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject objPrefab; /*!< A variable that holds a prefab to be spawned. */

    public ResourceManager manager; /*!< a variable that holds an instance of our resource manager. */

    [SerializeField]
    private float spwnTime = 1f;

    /// <summary>
    /// Start() is called before the first frame, Start() starts a spawning coroutine to spawn the prefab object every interval.
    /// </summary>
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ResourceManager>();
        StartCoroutine(SpawnEthan(spwnTime, objPrefab));
    }
    private IEnumerator SpawnEthan(float interval, GameObject person)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEthan = Instantiate(person, gameObject.transform.position, gameObject.transform.rotation);

        if (manager.pedCount < manager.pedLimit)
        {
            StartCoroutine(SpawnEthan(interval, person));
            manager.pedCount++;

        }
    }
}
