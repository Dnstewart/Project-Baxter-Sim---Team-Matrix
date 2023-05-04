using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Was used to test unity's testrunner.
/// </summary>
public class TestManager : MonoBehaviour
{

    [SerializeField]
    private GameObject thing; /*!< A gameobject that is spawned from the coroutine.  */
    [SerializeField]
    public float time = 5f; /*!< the time inbetween spawns. */

    private int count = 0;
    /// <summary>
    /// Start() starts a coroutine that spawns objects , this tests the spawnCar method.
    /// </summary>
    public void Start()
    {
        thing = GameObject.CreatePrimitive(PrimitiveType.Cube);
        StartCoroutine(SpawnCar(time, thing));
    }
    private IEnumerator SpawnCar(float interval, GameObject thing)
    {
        yield return new WaitForSeconds(interval);
        GameObject newCar = Instantiate(thing, gameObject.transform.position, gameObject.transform.rotation);
        count++;
        StartCoroutine(SpawnCar(interval, thing));
    }

    /// <summary>
    /// Gets the private variable called count.
    /// </summary>
    /// <returns>Returns an int variable with the contents of the variable count.</returns>
    public int getCount()
    {
        return count;
    }
}
