using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{

    [SerializeField]
    private GameObject thing;
    [SerializeField]
    public float time = 5f;

    private int count = 0;
    void Start()
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

    public int getCount()
    {
        return count;
    }
}
