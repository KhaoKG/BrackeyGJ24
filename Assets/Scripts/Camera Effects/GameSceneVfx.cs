using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GameSceneVfx : MonoBehaviour
{
    public Volume volume; // Reference to the Volume component containing your Chromatic Aberration effect
    public float maxIntensity = 1f; // Maximum intensity of chromatic aberration
    public float minIntensity = 0.1f; // Minimum intensity of chromatic aberration

    private ChromaticAberration chromaticAberration;
    private Player player;

    private void Start()
    {
        if (volume == null || !volume.profile.TryGet(out chromaticAberration))
        {
            Debug.LogError("Chromatic Aberration effect or Volume not set properly!");
            enabled = false;
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // Find and cache the player's health script
        if (player == null)
        {
            Debug.LogError("Player script not found!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        // Calculate missing health
        float missingHealth = Mathf.Clamp(player.maxHealth - player.health, 0f, player.maxHealth);
        float t = missingHealth / player.maxHealth;

        // Calculate intensity based on missing health
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);

        // Apply the chromatic aberration intensity
        chromaticAberration.intensity.value = intensity;
    }
}
