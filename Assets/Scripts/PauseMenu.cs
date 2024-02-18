using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    Image image;

    [SerializeField]
    float bgFadeAlpha = 0.7f;
    float baseBgAlpha;
    [SerializeField]
    float fadeSpeed = 1f;

    [SerializeField]
    FadeEffect screenFadeEffect;
    [SerializeField]
    GameObject settingsPanel;
    [SerializeField]
    GameObject scorePanel;
    [SerializeField]
    GameObject abilitiesPanel;

    Player player;

    void Start()
    {
        image = GetComponent<Image>();

        // Set children as not active until transition is done
        DeactivateChildren();

        // Keep base value of background alpha
        baseBgAlpha = bgFadeAlpha;

        // Keep track of player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        enabled = false;
    }

    void Update()
    {
        if (image.color.a != bgFadeAlpha) {
            Color color = image.color;
            color.a = Mathf.MoveTowards(color.a, bgFadeAlpha, fadeSpeed * Time.unscaledDeltaTime);
            image.color = color;
            return;
        }

        // Check if entering menu or leaving
        if (bgFadeAlpha > 0f) {
            // Activate menu and disable script
            ActivateChildren();
            enabled = false;
        } else {
            // Resume and disappear menu
            Resume();
        }
    }

    private void Resume() {
        Time.timeScale = 1f;
        AkSoundEngine.PostEvent("gameUnpaused", this.gameObject);
        enabled = false;
    }

    void DeactivateChildren() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }

    void ActivateChildren() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
    }

    public void Pause()
    {
        // Pause game
        Time.timeScale = 0f;
        AkSoundEngine.PostEvent("gamePaused", this.gameObject);

        // Set new target alpha
        bgFadeAlpha = baseBgAlpha;

        enabled = true;
    }

    public void PrepareResume() {
        DeactivateChildren();

        // Set new target alpha
        bgFadeAlpha = 0f;

        enabled = true;
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        DeactivateChildren();
        scorePanel.SetActive(false);
        abilitiesPanel.SetActive(false);
    }

    public void SettingsReturn()
    {
        settingsPanel.SetActive(false);
        ActivateChildren();
        scorePanel.SetActive(true);
        abilitiesPanel.SetActive(true);
    }

    public void Quit()
    {
        StartCoroutine(FadeAndLoadScene(0));
        AkSoundEngine.PostEvent("gameUnpaused", this.gameObject);
    }

    IEnumerator FadeAndLoadScene(int scene) {
        // Activate game object
        screenFadeEffect.TargetAlpha = 1f;

        yield return new WaitForSecondsRealtime(1f / screenFadeEffect.FadeSpeed);

        Time.timeScale = 1;

        SceneManager.LoadScene(scene);
    }
}
