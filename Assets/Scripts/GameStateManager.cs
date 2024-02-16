using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    // Combat
    [SerializeField] Player player;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] EnemyController enemyController;
    [SerializeField] FadeEffect screenFadeEffect;

    [Header("Game Over")]
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] bool isGameOver = false;

    [Header("Camera effects")]
    [SerializeField] HitStopEffect hitStopEffect;

    [Header("Waves")]
    [SerializeField] List<WaveSO> waves;

    GameStateSO gameStateData;

    private void Start() {
        // Load game data
        gameStateData = Resources.Load<GameStateSO>("ScriptableObjects/MainGameData");

        // Prepare initial wave
        enemySpawner.CurrentWave = waves[gameStateData.currentWave-1];
        enemySpawner.enabled = true;
    }

    public void CheckIfWaveOver() {
        if (!isGameOver && !enemySpawner.enabled) {
            // Play hit stop effect upon last enemy dying
            hitStopEffect.StartEffect();

            // Since spawner is disabled and there are no more living enemies, prepare key screen
            StartCoroutine(PrepareKeySelect());
        }
    }

    IEnumerator PrepareKeySelect() {
        player.DisableInput();

        // Short pause
        yield return new WaitForSeconds(1.5f);

        // Activate game object to fade screen
        screenFadeEffect.TargetAlpha = 1f;

        yield return new WaitForSeconds(1f / screenFadeEffect.FadeSpeed);

        ShowKeySelect();
    }

    void ShowKeySelect() {

        SceneManager.LoadScene(2);
    }

    public void GameOver() {
        isGameOver = true;
        player.DisableInput();

        // Stop enemies
        enemySpawner.StopSpawning();
        enemyController.OnGameOver();

        // Show game over menu
        gameOverMenu.SetActive(true);

        // Reset game state
        gameStateData.Reset();
    }
}
