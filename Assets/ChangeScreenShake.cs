using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeScreenShake : MonoBehaviour
{
    bool screenShakeOn;

    [SerializeField]
    Button toggleButton;

    private void Start() {
        // Starting value
        screenShakeOn = PlayerPrefs.GetInt("ScreenShake", 1) == 1;
        UpdateButton();
    }

    public void ToggleScreenShake() {
        screenShakeOn = !screenShakeOn;
        PlayerPrefs.SetInt("ScreenShake", screenShakeOn ? 1:0);
        UpdateButton();
    }

    void UpdateButton() {
        if (screenShakeOn) {
            toggleButton.GetComponent<Image>().color = toggleButton.colors.normalColor;
            toggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "ON";
        } else {
            toggleButton.GetComponent<Image>().color = toggleButton.colors.disabledColor;
            toggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "OFF";
        }
    }
}
