using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject KeySelectionScreen;
    [SerializeField] GameObject[] EnemySpawners;
    int currentNumEnemies = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(remainingEnemies.Length == 0)
        {
            // killed all enemies, round is done.
            KeySelectionScreen.SetActive(true);
        }
    }

    public void KeySelected(string Key)
    {
        // TODO: ADD THE KEY WHEREVER IT IS SUPPOSED TO GO

        // Reset the Enemy Spawners
        currentNumEnemies += 2;
        foreach(GameObject enemySpawner in EnemySpawners)
        {
            enemySpawner.GetComponent<EnemySpawner>().numEnemies = currentNumEnemies;
            Debug.Log("setting num enemies to spawn to " + enemySpawner.GetComponent<EnemySpawner>().numEnemies);
            enemySpawner.SetActive(true);
            enemySpawner.GetComponent<EnemySpawner>().SpawnEnemies();
        }

        // Reset the Key Selection Screen
        KeySelectionScreen.SetActive(false);
    }
}
