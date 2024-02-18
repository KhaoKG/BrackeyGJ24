using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour {
    [SerializeField]
    float targetAlpha;

    [SerializeField]
    float fadeSpeed;

    [SerializeField]
    Image img;

    public float TargetAlpha { get => targetAlpha; set { enabled = true; targetAlpha = value; } }
    public float FadeSpeed { get => fadeSpeed; set => fadeSpeed = value; }

    private void OnEnable() {
        img.enabled = true;
    }

    void Update() {
        Color color = img.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.unscaledDeltaTime);
        img.color = color;

        if (color.a == targetAlpha) {
            // Disable once target met
            enabled = false;
        }
    }

#if UNITY_EDITOR
    private void OnValidate() {
        SceneVisibilityManager.instance.Hide(gameObject, false);
    }
#endif
}
