using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    float targetAlpha;

    [SerializeField]
    float fadeSpeed;

    [SerializeField]
    Image img;

    public float TargetAlpha { get => targetAlpha; set => targetAlpha = value; }
    public float FadeSpeed { get => fadeSpeed; set => fadeSpeed = value; }

    void Update()
    {
        Color color = img.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
        img.color = color;

        if (color.a == targetAlpha) {
            // Disable once target met
            enabled = false;
        }
    }
}
