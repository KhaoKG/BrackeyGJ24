using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject KeySelectionScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(remainingEnemies.Length == 0)
        {
            // killed all enemies, round is done.
            KeySelectionScreen.SetActive(true);
        }
    }
}
