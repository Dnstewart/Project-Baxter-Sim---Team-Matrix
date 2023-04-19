using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ethanPrefab;

    public GameObject resourceManager;

    [SerializeField]
    public GameObject theBossPrefab;

    [SerializeField]
    private float ethanInterval = 1f;
    // Start is called before the first frame update
    private int count = 0;
    void Start()
    {
        StartCoroutine(SpawnEthan(ethanInterval, ethanPrefab));
        //StartCoroutine(SpawnEthan(ethanInterval, theBossPrefab));
        
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
