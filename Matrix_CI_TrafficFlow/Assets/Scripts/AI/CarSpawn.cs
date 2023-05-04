using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Team Matrix
/// </summary>
public class CarSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject car;
    [SerializeField]
    private float time = 10f;

    public ResourceManager manager;

    // Start is called before the first frame update
    public void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ResourceManager>();
    }


    private IEnumerator SpawnCar(float interval, GameObject car)
    {
        yield return new WaitForSeconds(interval);
        GameObject newCar = Instantiate(car, gameObject.transform.position, gameObject.transform.rotation);
        manager.carCount++;   
    }
}
