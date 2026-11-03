using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    [Header("Sensitivity")]
    public float minSensitivity = 0.1f;
    public float maxSensitivity = 5f;

    private const string VolumeKey = "MasterVolume";
    private const string SensitivityKey = "MouseSensitivity";

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        float savedSensitivity = PlayerPrefs.GetFloat(SensitivityKey, 1f);

        volumeSlider.value = savedVolume;

        sensitivitySlider.value = Mathf.InverseLerp(
            minSensitivity,
            maxSensitivity,
            savedSensitivity
        );

        ApplyVolume(savedVolume);
    }

    public void SetVolume(float value)
    {
        ApplyVolume(value);

        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }

    public void SetSensitivity(float sliderValue)
    {
        float sensitivity = Mathf.Lerp(
            minSensitivity,
            maxSensitivity,
            sliderValue
        );

        PlayerPrefs.SetFloat(SensitivityKey, sensitivity);
        PlayerPrefs.Save();
    }

    private void ApplyVolume(float value)
    {
        AudioListener.volume = value;
    }

    private void ApplyMusicVolume(float value)
    {
        AudioManager.instance.SetMusicVolume(value);
    }

    private void ApplySFXVolume(float value)
    {
        AudioManager.instance.SetSFXVolume(value);
    }

    public static float GetSensitivity()
    {
        return PlayerPrefs.GetFloat(SensitivityKey, 1f);
    }
}