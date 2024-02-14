using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject KeySelectionScreen;
    [SerializeField] EnemySpawner enemySpawner;

    [Header("Waves")]
    [SerializeField]
    List<WaveSO> waves;

    private void Start() {
        // Prepare initial wave
        enemySpawner.CurrentWave = waves[0];
        waves.RemoveAt(0);
        enemySpawner.enabled = true;
    }

    private void PrepareKeySelect() {
        KeySelectionScreen.SetActive(true);
        player.DisableInput();
    }

    public void CheckIfWaveOver() {
        if (!enemySpawner.enabled) {
            // Since spawner is disabled and there are no more living enemies, prepare key screen
            PrepareKeySelect();
        }
    }

    public void KeySelected(string Key) {
        // TODO: ADD THE KEY WHEREVER IT IS SUPPOSED TO GO

        // TODO Advance enemy spawner to next wave
        enemySpawner.CurrentWave = waves[0];
        waves.RemoveAt(0);
        enemySpawner.enabled = true;

        // Reset the Key Selection Screen
        KeySelectionScreen.SetActive(false);

        // Reactivate player
        player.EnableInput();
    }
}
