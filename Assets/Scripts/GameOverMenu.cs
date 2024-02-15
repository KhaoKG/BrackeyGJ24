using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    Image image;

    [SerializeField]
    float bgFadeAlpha = 0.7f;
    [SerializeField]
    float fadeSpeed = 1f;

    void Start()
    {
        image = GetComponent<Image>();

        // Set children as not active until transition is done
        DeactivateChildren();
    }

    // Update is called once per frame
    void Update()
    {
        if (image.color.a < bgFadeAlpha) {
            Color color = image.color;
            color.a = Mathf.MoveTowards(color.a, bgFadeAlpha, fadeSpeed * Time.deltaTime);
            image.color = color;
            return;
        }

        // Activate menu and disable script
        ActivateChildren();
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

    public void Retry() {
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        SceneManager.LoadScene(0);
    }
}
