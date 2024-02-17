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

    [Header("Pause Menu")]
    [SerializeField] PauseMenu pauseMenu;

    [Header("Game Over Menu")]
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
        int currentWave = Mathf.Min(gameStateData.currentWave - 1, waves.Count - 1);

        // Adding check in case we play fight scene before key choice
#if UNITY_EDITOR
        if (currentWave < 0) {
            currentWave = 0;
        }
#endif

        if (currentWave == waves.Count-1) {
            // Prepare randomized wave
            PrepareRandomWave();
        }

        enemySpawner.CurrentWave = waves[currentWave];
        enemySpawner.enabled = true;
        AbilityController.Instance.UpdateAbilitiesForRound();
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
        player.GetComponent<Collider2D>().enabled = false;

        // Short pause
        yield return new WaitForSeconds(1.5f);

        // Activate game object to fade screen
        screenFadeEffect.TargetAlpha = 1f;

        yield return new WaitForSeconds(1f / screenFadeEffect.FadeSpeed);

        player.GetComponent<Collider2D>().enabled = true;

        ShowKeySelect();
    }

    void ShowKeySelect() {
        SceneManager.LoadScene("DoorBonus");
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

    void PrepareRandomWave() {
        WaveSO wave = waves[waves.Count - 1];

        // Modifies random wave with the amount of current waves
        int modifier = gameStateData.currentWave - waves.Count;

        // Add modifier to enemy spawner
        enemySpawner.WaveModifier = modifier;

        while (modifier > 0) {
            wave.spawns[Random.Range(0, wave.spawns.Count)].enemies.Add(EnemySpawnEnum.Random);

            modifier--;
        }

        // Randomize spawn delays
        foreach (WaveSO.TimeSpawn spawn in wave.spawns) {
            spawn.delay += Random.Range(-1f, 1f);
        }
    }

    public void ProcessPause() {
        if (IsPaused()) {
            pauseMenu.PrepareResume();
        }
        else {
            // Show pause menu
            pauseMenu.Pause();
        }
    }

    public bool IsPaused() {
        return Time.timeScale == 0f;
    }
}
