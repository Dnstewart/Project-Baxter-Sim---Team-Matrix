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
    private GameObject objPrefab;

    public GameObject resourceManager; /*!< This variable holds an instance of a resource manager object for updating the UI. */

    [SerializeField]
    private float spwnTime = 1f;
    private int count = 0;

    /// <summary>
    /// Start() is called before the first frame, Start() starts a spawning coroutine to spawn the prefab object every interval.
    /// </summary>
    void Start()
    {
        StartCoroutine(SpawnEthan(spwnTime, objPrefab));
    }
    private IEnumerator SpawnEthan(float interval, GameObject person)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEthan = Instantiate(person, gameObject.transform.position, gameObject.transform.rotation);

        count++;
        if (count != 250)
        {
            StartCoroutine(SpawnEthan(interval, person));
            resourceManager.GetComponent<ResourceManager>().pedCount++;

        }
    }
}
