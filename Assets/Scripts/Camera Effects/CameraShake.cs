using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;

    [SerializeField]
    float shakeAmplitude = 0f;
    float currentAmplitude = 0f;

    [SerializeField]
    float duration = 0f;

    private void Awake() {
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float amount, float duration) {
        // Check if player wants camera shake effect
        if (PlayerPrefs.GetInt("ScreenShake", 1) == 0) {
            return;
        }

        enabled = true;

        shakeAmplitude = Mathf.Max(shakeAmplitude, amount);
        //if (GameController.controller.ScreenShakeOn) {
            currentAmplitude = Mathf.Max(currentAmplitude, amount);
        //}
        //else {
        //    currentAmplitude = 0;
        //}
        this.duration = Mathf.Max(this.duration, duration);
    }

    void Update()
    {
        currentAmplitude =
            Mathf.MoveTowards(currentAmplitude, 0,
            shakeAmplitude / duration * Time.deltaTime);

        noise.m_AmplitudeGain = currentAmplitude;

        // Check if shaking is over
        if (currentAmplitude == 0) {
            enabled = false;
            duration = 0;
            shakeAmplitude = 0;
        }
    }
}
