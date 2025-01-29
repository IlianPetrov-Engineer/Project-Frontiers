using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;

public class VolumeSettingsManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [Tooltip("Assign the Audio Mixer here.")]
    public AudioMixer audioMixer;

    [Header("Slider Container")]
    [Tooltip("Parent object containing all volume sliders. The script will automatically detect them.")]
    public Transform slidersParent;

    [Tooltip("Automatically detected sliders.")]
    public List<Slider> sliders = new List<Slider>();

    void Start()
    {
        // Auto-detect sliders inside slidersParent
        sliders.AddRange(slidersParent.GetComponentsInChildren<Slider>());

        foreach (Slider slider in sliders)
        {
            string groupName = slider.gameObject.name; // Use the GameObject name as the Audio Mixer parameter

            // Load saved volume, default to 0.75 if not set
            float savedVolume = PlayerPrefs.GetFloat(groupName, 0.75f);
            slider.value = savedVolume;

            // Apply volume to mixer
            SetVolume(groupName, savedVolume);

            // Add listener to update volume when slider changes
            slider.onValueChanged.AddListener(value => SetVolume(groupName, value));
        }
    }

    private void SetVolume(string groupName, float sliderValue)
    {
        // Convert slider value to dB (-80 dB to 0 dB range)
        float dB = Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20; // Avoid log(0) errors
        audioMixer.SetFloat(groupName, dB);

        // Save the volume setting
        PlayerPrefs.SetFloat(groupName, sliderValue);
        PlayerPrefs.Save();
    }
}