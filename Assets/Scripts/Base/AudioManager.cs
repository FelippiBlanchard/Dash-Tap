using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private bool initializeOnAwake;

    [SerializeField] private AudioMixer mixerFX;
    [SerializeField] private AudioMixer mixerBGM;

    [SerializeField] private Slider sliderVolumeFX;
    [SerializeField] private Slider sliderVolumeBGM;

    [SerializeField] private ToggleButton[] toggleFX;
    [SerializeField] private ToggleButton[] toggleBGM;

    private float defaultVolume = 0.45f;

    private bool enabledFX;
    private bool enabledBGM;

    private void Start()
    {
        if(initializeOnAwake)
            Initialize();
    }

    public void SetVolumeFXMixer(float value)
    {
        var volume = ConvertValue(value);
        mixerFX.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("VolumeFX", value);
        enabledFX = value > 0f;
        InitializeTogglesVolumes();
    }
    public void SetVolumeBGMMixer(float value)
    {
        var volume = ConvertValue(value);
        mixerBGM.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("VolumeBGM", value);
        enabledBGM = value > 0;
        InitializeTogglesVolumes();
    }

    private float ConvertValue(float value)
    {
        var volume = 0f;
        if (value < 0.5f)
        {
            volume = value * 160 - 80;
        }
        if (value >= 0.5f)
        {
            volume = value * 40 - 20;
        }
        return volume;
    }

    public void Initialize()
    {
        var valueFX = PlayerPrefs.GetFloat("VolumeFX", defaultVolume);
        SetVolumeFXMixer(valueFX);

        var valueBGM = PlayerPrefs.GetFloat("VolumeBGM", defaultVolume);
        SetVolumeBGMMixer(valueBGM);

        InitializeSliderVolumeBGM(valueBGM);
        InitializeSliderVolumeFX(valueFX);
        enabledFX = valueFX > 0;
        enabledBGM = valueBGM > 0;
        InitializeTogglesVolumes();

    }

    private void InitializeSliderVolumeFX(float valueFX)
    {
        if (sliderVolumeFX != null)
        {
            sliderVolumeFX.value = valueFX;
            sliderVolumeFX.onValueChanged.AddListener(SetVolumeFXMixer);
        }
    }
    private void InitializeSliderVolumeBGM(float valueBGM)
    {
        if (sliderVolumeBGM != null)
        {
            sliderVolumeBGM.value = valueBGM;
            sliderVolumeBGM.onValueChanged.AddListener(SetVolumeBGMMixer);
        }
    }

    private void InitializeTogglesVolumes()
    {
        if(toggleFX.Length > 0)
        {
            foreach (var toggle in toggleFX)
            {
                toggle.off.SetActive(!enabledFX);
            }
        }

        if (toggleBGM.Length > 0)
        {
            foreach (var toggle in toggleBGM)
            {
                toggle.off.SetActive(!enabledBGM);
            }
        }
    }

    public void ToggleAudioMixerFX()
    {
        var value = enabledFX ? 0f : defaultVolume;
        enabledFX = !enabledFX;
        if (sliderVolumeFX != null)
        {
            sliderVolumeFX.value = value;
        }
        else
        {
            InitializeTogglesVolumes();
            SetVolumeFXMixer(value);
        }
    }
    public void ToggleAudioMixerBGM()
    {
        var value = enabledBGM ? 0f : defaultVolume;
        enabledBGM = !enabledBGM;
        InitializeTogglesVolumes();
        if (sliderVolumeBGM != null)
        {
            sliderVolumeBGM.value = value;
        }
        else
        {
            SetVolumeBGMMixer(value);
        }
    }
}

[Serializable]
public class ToggleButton
{
    public GameObject on;
    public GameObject off;
}
