using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps track of enemies alive in the arena
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameStateManager gameStateManager;
    [SerializeField]
    Score score;
    List<Enemy> enemiesAlive = new List<Enemy>();

    public void OnSpawnEnemy(Enemy enemy) {
        enemiesAlive.Add(enemy);

        // Prepares enemy to report on death
        enemy.EnemyController = this;
    }

    public void OnEnemyDeath(Enemy enemy) {
        // Add enemy dying effect to enemy
        enemy.gameObject.AddComponent<EnemyDyingEffect>();

        // Update score
        score.AddScore(enemy.Score);

        enemiesAlive.Remove(enemy);
        Debug.Log("Enemy died, new count it " + enemiesAlive.Count);

        // Inform game state manager in case there are no more enemies
        if (enemiesAlive.Count == 0) {
            gameStateManager.CheckIfWaveOver();
        }
    }

    public void OnGameOver() {
        foreach (Enemy enemy in enemiesAlive) {
            enemy.enabled = false;
        }
    }
}
