using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    [SerializeField] Player player;
    [SerializeField] GameObject KeySelectionScreen;
    [SerializeField] EnemySpawner enemySpawner;
    int currentNumEnemies = 4;

    void Update()
    {
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(remainingEnemies.Length == 0)
        {
            // killed all enemies, round is done.
            KeySelectionScreen.SetActive(true);
            player.DisableInput();
        }
    }

    //public void KeySelected(string Key)
    //{
    //    // TODO: ADD THE KEY WHEREVER IT IS SUPPOSED TO GO

    //    // Reset the Enemy Spawners
    //    currentNumEnemies += 2;
    //    foreach(GameObject enemySpawner in enemySpawner)
    //    {
    //        enemySpawner.GetComponent<EnemySpawner>().numEnemies = currentNumEnemies;
    //        Debug.Log("setting num enemies to spawn to " + enemySpawner.GetComponent<EnemySpawner>().numEnemies);
    //        enemySpawner.SetActive(true);
    //        enemySpawner.GetComponent<EnemySpawner>().SpawnEnemies();
    //    }

    //    // Reset the Key Selection Screen
    //    KeySelectionScreen.SetActive(false);

    //    // Reactivate player
    //    player.EnableInput();
    //}
}
