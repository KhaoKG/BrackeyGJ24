using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeVolumeLevel : MonoBehaviour
{
    public const string SFX_VOLUME_PREF = "gameEffectVolume";
    public const string MUSIC_VOLUME_PREF = "gameMusicVolume";
    public const string MASTER_VOLUME_PREF = "gameMasterVolume";
    public const float DEFAULT_VOLUME = 80f;

    [SerializeField]
    public Slider EffectSlider;
    [SerializeField]
    public TMP_Text EffectSliderValue;
    [SerializeField]
    public Slider musicSlider;
    [SerializeField]
    public TMP_Text musicSliderValue;
    [SerializeField]
    public Slider masterSlider;
    [SerializeField]
    public TMP_Text masterSliderValue;
    [SerializeField]
    public float gameEffectVolume;
    [SerializeField]
    public float gameMusicVolume;
    [SerializeField]
    public float gameMasterVolume;

    private void Start()
    {
        float currentSFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_PREF, DEFAULT_VOLUME);
        EffectSlider.value = currentSFXVolume;
        AkSoundEngine.SetRTPCValue(SFX_VOLUME_PREF, currentSFXVolume);
        EffectSliderValue.text = gameEffectVolume.ToString();

        float currentMusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_PREF, DEFAULT_VOLUME);
        musicSlider.value = currentMusicVolume;
        AkSoundEngine.SetRTPCValue(MUSIC_VOLUME_PREF, currentMusicVolume);
        musicSliderValue.text = gameMusicVolume.ToString();

        float currentMasterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_PREF, DEFAULT_VOLUME);
        masterSlider.value = currentMasterVolume;
        AkSoundEngine.SetRTPCValue(MASTER_VOLUME_PREF, currentMasterVolume);
        masterSliderValue.text = gameMasterVolume.ToString();
    }

    public void SetEffectVolume()
    {
        gameEffectVolume = EffectSlider.value;
        PlayerPrefs.SetFloat(SFX_VOLUME_PREF, gameEffectVolume);
        AkSoundEngine.SetRTPCValue(SFX_VOLUME_PREF, gameEffectVolume);
        EffectSliderValue.text = gameEffectVolume.ToString();
    }

    public void SetMusicVolume()
    {
        gameMusicVolume = musicSlider.value;
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PREF, gameMusicVolume);
        AkSoundEngine.SetRTPCValue(MUSIC_VOLUME_PREF, gameMusicVolume);
        musicSliderValue.text = gameMusicVolume.ToString();
    }
    public void SetMasterVolume()
    {
        gameMasterVolume = masterSlider.value;
        PlayerPrefs.SetFloat(MASTER_VOLUME_PREF, gameMasterVolume);
        AkSoundEngine.SetRTPCValue(MASTER_VOLUME_PREF, gameMasterVolume);
        masterSliderValue.text = gameMasterVolume.ToString();
    }
}