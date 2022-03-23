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

    private void Start()
    {
        _bgmValue = DataManager.instance.BGMmasterSoundVolume;
        _effValue = DataManager.instance.EFFmasterSoundVolume;

        bgmSlider.value = _bgmValue;
        effSlider.value = _effValue;
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
}
