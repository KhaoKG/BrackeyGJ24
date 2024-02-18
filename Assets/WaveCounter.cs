using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveCounter : MonoBehaviour
{
    [SerializeField] GameStateSO gameState;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Wave: " + gameState.currentWave;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
