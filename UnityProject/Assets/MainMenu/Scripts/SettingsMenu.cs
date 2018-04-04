using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public AudioMixer audioMixer;
    public Slider masterVolSlider; public Slider musicVolSlider;
    public Slider sfxVolSlider;
    void Start()
    {
        masterVolSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicVolSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolSlider.value = PlayerPrefs.GetFloat("SfxVolume");

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i+=2)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
        DontDestroyOnLoad(audioMixer);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
        DontDestroyOnLoad(audioMixer);
    }
    public void SetSoundEffectsVolume(float volume)
    {
        audioMixer.SetFloat("soundEffectsVolume", volume);
        DontDestroyOnLoad(audioMixer);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    void Update()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolSlider.value);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolSlider.value);

    }
}
