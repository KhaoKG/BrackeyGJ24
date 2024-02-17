using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDyingEffect : MonoBehaviour
{
    Material material;

    SpriteRenderer spriteRenderer;

    float effectSpeed = 1f;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    private void Update() {

        float curThreshold = material.GetFloat("_Disintegrate_Threshold");

        if (curThreshold <= 0) {
            Destroy(gameObject);
            return;
        }
        material.SetFloat("_Disintegrate_Threshold", Mathf.MoveTowards(curThreshold, 0, Time.deltaTime * effectSpeed));
    }

    private void OnDestroy() {
        // Removes material instance from memory
        Destroy(material);
    }
}
