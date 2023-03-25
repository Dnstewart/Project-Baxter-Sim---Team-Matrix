using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ethanPrefab;
    [SerializeField]
    private float ethanInterval = 2.5f;
    // Start is called before the first frame update
    private int count = 0;
    void Start()
    {
        StartCoroutine(spawnEthan(ethanInterval, ethanPrefab));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEthan(float interval, GameObject person)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEthan = Instantiate(person, new Vector3(Random.Range(-5f, 5), 1, 0), Quaternion.identity);
        count++;
        if (count != 10)
        {
            StartCoroutine(spawnEthan(interval, person));
        }
    }
}