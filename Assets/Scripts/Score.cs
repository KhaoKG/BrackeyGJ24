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

    void FixedUpdate()
    {
        if(streakTimer > -1)
        {
            streakTimer--;
        }
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        streak += 10;

        if(streakTimer > 0)
        {
            score += streak;
        }

        GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        streakTimer = 30f;
    }
}
