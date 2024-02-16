using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeySelection : MonoBehaviour {
    GameStateSO gameStateData;

    [SerializeField] 
    FadeEffect screenFadeEffect;

    private void Start() {
        // Load game data
        gameStateData = Resources.Load<GameStateSO>("ScriptableObjects/MainGameData");
    }

    public void KeySelected(string Key) {
        // TODO: ADD THE KEY WHEREVER IT IS SUPPOSED TO GO

        // Return to fight next wave
        StartCoroutine(PrepareNextWave());
    }

    IEnumerator PrepareNextWave() {
        // Activate game object to fade screen
        screenFadeEffect.TargetAlpha = 1f;

        yield return new WaitForSeconds(1f / screenFadeEffect.FadeSpeed);

        GoToNextWave();
    }

    void GoToNextWave() {
        gameStateData.currentWave++;
        SceneManager.LoadScene(1);
    }
}
