using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteBlinkEffect : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField]
    float blinkSpeed = 1f;

    [SerializeField]
    float minAlpha = 0f;
    [SerializeField]
    float maxAlpha = 1f;
    float baseAlpha;
    float targetAlpha;

    public float BlinkSpeed { get => blinkSpeed; set => blinkSpeed = value; }
    public float MinAlpha { get => minAlpha; set => minAlpha = value; }
    public float MaxAlpha { get => maxAlpha; set => maxAlpha = value; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enabled = false;
    }

    void Update()
    {
        Color color = spriteRenderer.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, blinkSpeed * Time.deltaTime);
        spriteRenderer.color = color;

        if (color.a == targetAlpha) {
            targetAlpha = (targetAlpha == minAlpha ? maxAlpha : minAlpha);
        }
    }

    public void StartBlinking() {
        baseAlpha = spriteRenderer.color.a;
        targetAlpha = minAlpha;
        enabled = true;
    }

    public void StopBlinking() {
        Color color = spriteRenderer.color;
        color.a = baseAlpha;
        spriteRenderer.color = color;
        enabled = false;
    }
}
