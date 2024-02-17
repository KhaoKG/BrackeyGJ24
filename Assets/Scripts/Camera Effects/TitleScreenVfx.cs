using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class TitleScreenVfx : MonoBehaviour
{
    public Volume volume; // Reference to the Volume component containing your Chromatic Aberration effect
    public float fadeDuration = 2f; // Duration of the fade in seconds

    private ChromaticAberration chromaticAberration;
    private float currentFadeTime = 0f;
    private bool fadingIn = true;

    private void Start()
    {
        if (volume == null || !volume.profile.TryGet(out chromaticAberration))
        {
            Debug.LogError("Chromatic Aberration effect or Volume not set properly!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        // Update fade
        if (fadingIn)
        {
            currentFadeTime += Time.deltaTime;
            if (currentFadeTime >= fadeDuration)
            {
                currentFadeTime = fadeDuration;
                fadingIn = false;
            }
        }
        else
        {
            currentFadeTime -= Time.deltaTime;
            if (currentFadeTime <= 0f)
            {
                currentFadeTime = 0f;
                fadingIn = true;
            }
        }

        // Calculate t value for lerping between 0 and 1 based on fade time and duration
        float t = currentFadeTime / fadeDuration;

        // Apply the chromatic aberration intensity
        chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, t);
    }
}
