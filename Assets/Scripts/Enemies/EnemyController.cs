using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps track of enemies alive in the arena
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameStateManager gameStateManager;
    List<Enemy> enemiesAlive = new List<Enemy>();

    public void OnSpawnEnemy(Enemy enemy) {
        enemiesAlive.Add(enemy);

        // Prepares enemy to report on death
        enemy.EnemyController = this;
    }

    public void OnEnemyDeath(Enemy enemy) {
        // Add enemy dying effect to enemy
        enemy.gameObject.AddComponent<EnemyDyingEffect>();

        enemiesAlive.Remove(enemy);

        // Inform game state manager in case there are no more enemies
        if (enemiesAlive.Count == 0) {
            gameStateManager.CheckIfWaveOver();
            AkSoundEngine.PostEvent("playerLastHit", this.gameObject);
        }
    }

    public void OnGameOver() {
        foreach (Enemy enemy in enemiesAlive) {
            enemy.enabled = false;
        }
    }
}
