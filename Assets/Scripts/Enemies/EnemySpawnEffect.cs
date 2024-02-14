using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnEffect : MonoBehaviour
{
    Material material;

    float spawnEffectSpeed = 1f;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update() {
        float curThreshold = material.GetFloat("_Threshold");

        if (curThreshold >= 1) {
            GetComponent<Enemy>().OnSpawnReady();
            Destroy(this);
            return;
        } 
        material.SetFloat("_Threshold", Mathf.MoveTowards(curThreshold, 1, Time.deltaTime * spawnEffectSpeed));
    }
}
