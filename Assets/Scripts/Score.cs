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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(streakTimer > -1)
        {
            streakTimer--;
        }
    }

    public void SetScore(int addScore)
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
