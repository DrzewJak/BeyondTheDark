using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AdvancedSettingsManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Display Settings")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Dropdown resolutionDropdown;

    [Header("Graphics Settings")]
    [SerializeField] private Dropdown qualityDropdown;

    private Resolution[] supportedResolutions;

    private void Start()
    {
        InitializeResolutionSettings();
        LoadSettings();
    }

    private void InitializeResolutionSettings()
    {
        // Populate resolution dropdown
        supportedResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < supportedResolutions.Length; i++)
        {
            string resolutionOption = $"{supportedResolutions[i].width} x {supportedResolutions[i].height}";
            resolutionOptions.Add(resolutionOption);

            // Check if this is the current screen resolution
            if (supportedResolutions[i].width == Screen.currentResolution.width &&
                supportedResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = supportedResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    public void SetQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }

    private void LoadSettings()
    {
        // Load Audio Settings
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;

        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);

        // Load Display Settings
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;

        int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        resolutionDropdown.value = resolutionIndex;
        SetResolution(resolutionIndex);

        // Load Graphics Settings
        int qualityLevel = PlayerPrefs.GetInt("QualityLevel", 3);
        qualityDropdown.value = qualityLevel;
        SetQualityLevel(qualityLevel);
    }

    public void ResetToDefaultSettings()
    {
        // Reset Audio
        SetMasterVolume(1f);
        SetMusicVolume(1f);
        SetSFXVolume(1f);

        masterVolumeSlider.value = 1f;
        musicVolumeSlider.value = 1f;
        sfxVolumeSlider.value = 1f;

        // Reset Display
        SetFullscreen(true);
        fullscreenToggle.isOn = true;

        // Reset to default resolution (usually the highest available)
        int defaultResolutionIndex = supportedResolutions.Length - 1;
        resolutionDropdown.value = defaultResolutionIndex;
        SetResolution(defaultResolutionIndex);

        // Reset Quality
        int defaultQualityLevel = 3; // Medium quality
        qualityDropdown.value = defaultQualityLevel;
        SetQualityLevel(defaultQualityLevel);

        // Clear PlayerPrefs if you want to completely reset
        PlayerPrefs.DeleteAll();
    }
}