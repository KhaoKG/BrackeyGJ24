using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopEffect : MonoBehaviour {
    [SerializeField]
    CinemachineVirtualCamera vcam;

    float originalSize;

    [SerializeField]
    float effectSize = 0f;

    [SerializeField]
    float duration = 1f;

    [SerializeField]
    float effectTimeScale = 0.1f;

    [SerializeField]
    float zoomSpeed = 5f;

    Coroutine endEffectCoroutine = null;

    bool zoomingIn = false;
    bool zoomingOut = false;

    [SerializeField]
    CinemachineBrain brain;

    private void Start() {
        originalSize = vcam.m_Lens.OrthographicSize;

        enabled = false;
    }

    public void StartEffect() {
        //vcam.m_Follow = FindObjectOfType<Enemy>().transform;

        enabled = true;
        // Slow time
        Time.timeScale = effectTimeScale;

        //// Approach camera
        //vcam.m_Lens.OrthographicSize = effectSize;

        if (endEffectCoroutine != null) {
            StopCoroutine(endEffectCoroutine);
            endEffectCoroutine = null;
        }

        // Slow down sound effect
        AkSoundEngine.PostEvent("slowDown", this.gameObject);

        zoomingIn = true;

        brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;

        endEffectCoroutine = StartCoroutine(EndEffect());
    }

    private void Update() {
        if (zoomingIn) {
            float currentSize = Mathf.MoveTowards(vcam.m_Lens.OrthographicSize, effectSize, Time.unscaledDeltaTime * zoomSpeed);

            // Approach camera
            vcam.m_Lens.OrthographicSize = currentSize;

            if (currentSize == effectSize) {
                zoomingIn = false;
            }
        } else if (zoomingOut) {
            float currentSize = Mathf.MoveTowards(vcam.m_Lens.OrthographicSize, originalSize, Time.unscaledDeltaTime * zoomSpeed);

            // Approach camera
            vcam.m_Lens.OrthographicSize = currentSize;

            if (currentSize == originalSize) {
                zoomingOut = false;
                brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;

                enabled = false;
            }
        }
    }

    IEnumerator EndEffect() {
        yield return new WaitForSecondsRealtime(duration);

        // Return time and ortographic size
        Time.timeScale = 1;

        zoomingOut = true;
        endEffectCoroutine = null;

        // Sound effect
        AkSoundEngine.PostEvent("speedUp", this.gameObject);
    }
}
