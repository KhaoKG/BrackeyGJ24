using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject KeySelectionScreen;
    [SerializeField] EnemySpawner enemySpawner;

    [Header("Camera effects")]
    [SerializeField]  HitStopEffect hitStopEffect;

    [Header("Waves")]
    [SerializeField] List<WaveSO> waves;

    private void Start() {
        // Prepare initial wave
        enemySpawner.CurrentWave = waves[0];
        waves.RemoveAt(0);
        enemySpawner.enabled = true;
    }

    public void CheckIfWaveOver() {
        if (!enemySpawner.enabled) {
            // Play hit stop effect upon last enemy dying
            hitStopEffect.StartEffect();

            // Since spawner is disabled and there are no more living enemies, prepare key screen
            StartCoroutine(PrepareKeySelect());
        }
    }

    IEnumerator PrepareKeySelect() {
        player.DisableInput();

        yield return new WaitForSeconds(1.5f);

        ShowKeySelect();
    }




    private void ShowKeySelect() {
        KeySelectionScreen.SetActive(true);
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
