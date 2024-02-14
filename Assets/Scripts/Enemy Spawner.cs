using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] public int numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoSpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DoSpawnEnemy()
    {
        Debug.Log("I'm beginning spawning enemies");
        for (int i = 0; i < numEnemies; i++)
        {
            Debug.Log("spawning enemy");
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(Random.Range(2, 8));
        }
        gameObject.SetActive(false);
    }

    public void SpawnEnemies()
    {
        StartCoroutine(DoSpawnEnemy());
    }
}
