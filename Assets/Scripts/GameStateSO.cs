using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState")]
public class GameStateSO : ScriptableObject
{
    public int score = 0;

    public int maxHealth = 10;

    [Min(1)]
    public int currentWave = 0;

    // TODO Add abilities/keys

    public void Reset() {
        score = 0;
        maxHealth = 10;
        currentWave = 0;
    }

    private void OnEnable() {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
    }

#if UNITY_EDITOR
    private void OnPlayModeStateChanged(PlayModeStateChange playMode) {
        if (playMode == PlayModeStateChange.ExitingPlayMode) { 
                EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
                Reset();
        }
    }
#endif

}