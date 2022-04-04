using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    float _bgmValue;
    float _effValue;

    public Slider bgmSlider;
    public Slider effSlider;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        _bgmValue = DataManager.instance.BGMmasterSoundVolume;
        _effValue = DataManager.instance.EFFmasterSoundVolume;

        bgmSlider.value = _bgmValue;
        effSlider.value = _effValue;

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int curResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " * " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                curResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = curResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void BGMControl()
    {
        _bgmValue = bgmSlider.value;
        DataManager.instance.BGMmasterSoundVolume = _bgmValue;
        
        PlayerPrefs.SetFloat("BGM", _bgmValue);
        MGSound.instance.BGMVol();  // 중간 조절

    }
    public void EFFControl()
    {
        _effValue = effSlider.value;
        DataManager.instance.EFFmasterSoundVolume = _effValue;

        PlayerPrefs.SetFloat("EFF", _effValue);
    }

    //// Graphics Quality
    //public void SetQuality(int qualityIndex)
    //{
    //    QualitySettings.SetQualityLevel(qualityIndex);
    //}

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetScreenSize()
    {
        CanvasScaleManager.instance.SetScreenMode();
    }


}
