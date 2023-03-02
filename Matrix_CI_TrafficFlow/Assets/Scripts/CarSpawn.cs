using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject car;
    [SerializeField]
    private float time = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCar(time, car));
    }
    private IEnumerator SpawnCar(float interval, GameObject car)
    {
        yield return new WaitForSeconds(interval);
        GameObject newCar = Instantiate(car, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine(SpawnCar(interval, car));
    }
}
