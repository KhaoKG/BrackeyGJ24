using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera vcam;

    [SerializeField]
    float shakeAmplitude = 0f;
    float currentAmplitude = 0f;

    [SerializeField]
    float duration = 0f;

    public void Shake(float amount, float duration) {
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

        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = currentAmplitude;

        // Check if shaking is over
        if (currentAmplitude == 0) {
            enabled = false;
            duration = 0;
            shakeAmplitude = 0;
        }
    }
}
