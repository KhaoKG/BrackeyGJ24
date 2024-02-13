using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoSpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoSpawnEnemy()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(Random.Range(2, 8));
        }
    }
}
