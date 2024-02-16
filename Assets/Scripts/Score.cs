using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int score = 0;
    int streak = 0;
    float streakTimer = 0f;

    GameStateSO gameStateData;

    TextMeshProUGUI textElement;

    private void Start() {
        // Load game data score
        gameStateData = Resources.Load<GameStateSO>("ScriptableObjects/MainGameData");
        score = gameStateData.score;

        // Get score text component
        textElement = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    void FixedUpdate()
    {
        if(streakTimer > -1)
        {
            streakTimer--;
        }
    }

    public void AddScore(int addScore) {
        score += addScore;

        // Calculate streak
        streak += 10;

        if (streakTimer > 0) {
            score += streak;
        }
        streakTimer = 30f;

        gameStateData.score = score;
        UpdateText();
    }

    private void UpdateText() {
        textElement.text = "Score: " + score;
    }
}
