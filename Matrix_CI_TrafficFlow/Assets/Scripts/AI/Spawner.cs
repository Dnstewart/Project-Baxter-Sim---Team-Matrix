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
        StartCoroutine(spawnEthan(ethanInterval, ethanPrefab));
        StartCoroutine(spawnEthan(ethanInterval, theBossPrefab));
        
    }
    private IEnumerator spawnEthan(float interval, GameObject person)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEthan = Instantiate(person, gameObject.transform.position, gameObject.transform.rotation);
        GameObject newBoss = Instantiate(person, gameObject.transform.position, gameObject.transform.rotation);

        count++;
        if (count != 250)
        {
            StartCoroutine(spawnEthan(interval, person));
            StartCoroutine(spawnEthan(interval, person));
            resourceManager.GetComponent<ResourceManager>().pedCount++;
            resourceManager.GetComponent<ResourceManager>().pedCount++;

        }
    }
}
